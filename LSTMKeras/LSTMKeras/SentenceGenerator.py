from __future__ import print_function
import numpy as np
import os
import scipy
from six.moves import cPickle

#import gensim library
import gensim
from gensim.models.doc2vec import LabeledSentence

#import keras library
from keras.models import load_model
import spacy
nlp = spacy.load('en_core_web_sm')

print(np)


data_dir = 'S:\CrispyPancake\data'
save_dir = os.path.join('S:\CrispyPancake', "save")
vocab_file = os.path.join(save_dir, "words_vocab.pk1")
file_list = ["105"]
sequences_step = 1

print("loading doc2Vec model...")
d2v_model = gensim.models.doc2vec.Doc2Vec.load(os.path.join(data_dir, 'doc2vec.w2v'))
print("model laoded!")

print("loading vocabulary...")

with open(vocab_file, 'rb') as f:
    words, vocab, vocabulary_inv = cPickle.load(f)

vocab_size = len(words)
print("vocabulary loaded!")

print("loading word prediction model...")
model = load_model(save_dir + "/" + 'my_model_gen_sentences_lstm.final.hdf5')
print("model loaded!")
print("loading sentence selection model...")
model_sequence = load_model(save_dir + "/" + 'my_model_sequence_lstm.final.hdf5')
print("model loaded!")

def sample(preds, temperature=1.0):
    preds = np.asarray(preds).astype('float64')
    preds = np.log(preds) / temperature
    exp_preds = np.exp(preds)
    preds = exp_preds / np.sum(exp_preds)
    probas = np.random.multinomial(1, preds, 1)
    return np.argmax(probas)

def create_seed(seed_sentences, nb_words_in_seq=20, verbose=False):
    generated = ''
    sentence = []
    for i in range(nb_words_in_seq):
        sentence.append("the")

    seed = seed_sentences.split()

    if verbose == True : print("seed: ", seed)

    for i in range(len(sentence)):
        sentence[nb_words_in_seq-i-1]=seed[len(seed)-i-1]

    generated += ' '.join(sentence)

    if verbose == True: print('Generating text with the following seed: "' + ' '.join(sentence) + '"')
    return [generated, sentence]

def generate_phrase(sentence, max_words=50, nb_words_in_seq=20, temperature=1, verbose=False):
    generated = ""
    words_number = max_words -1
    punctuation = [".", "?", "!", ":", "..."]
    seq_length = nb_words_in_seq
    is_punct=False

    for i in range(words_number):
        x = np.zeros((1, seq_length, vocab_size))
        for t, word in enumerate(sentence):
            x[0, nb_words_in_seq - len(sentence)+t, vocab[word]] = 1

        preds = model.predict(x, verbose=0)[0]
        next_index = sample(preds, temperature)
        next_word = vocabulary_inv[next_index]

        if verbose == True:
            predv = np.array(preds)
            wi = predv.argsort()[-3:][::-1]
            print("potential next words: ", vocabulary_inv[wi[0]], vocabulary_inv[wi[1]], vocabulary_inv[wi[2]])

        if is_punct == False:
            if next_word in punctuation:
                is_punct = True
            generated += " " + next_word

            sentence = sentence[1:] + [next_word]

    return(generated, sentence)

def define_phrases_candidates(sentence, max_words = 50, 
                              nb_words_in_seq = 20,
                              temperature = 1,
                              nb_candidates_sents=10,
                              verbose = False):
    phrase_candidate = []
    generated_sentence = ""
    for i in range(nb_candidates_sents):
        generated_sentence, new_sentence = generate_phrase(sentence,
                                                           max_words = max_words,
                                                           nb_words_in_seq = nb_words_in_seq,
                                                           temperature = temperature,
                                                           verbose = False)
        phrase_candidate.append([generated_sentence, new_sentence])

    if verbose == True : 
        for phrase in phrase_candidate:
            print("   ", phrase[0])

    return phrase_candidate

def create_sentences(doc):
    punctuation = [".", "?", "!", ":", "..."]
    sentences = []
    sent = []
    for word in doc:
        if word.text not in punctuation:
            if word.text not in ("\n", "\n\n", '\u2009', '\xa0'):
                sent.append(word.text.lower())
        else:
            sent.append(word.text.lower())
            if len(sent) > 1:
                sentences.append(sent)
            sent = []
    return sentences

def generate_training_vector(sentences_list, verbose = False):
    if verbose == True: print("generate vectors for each sentence...")
    seq = []
    V = []

    for s in sentences_list:
        v = d2v_model.infer_vector(create_sentences(nlp(s))[0], alpha=0.001, min_alpha=0.001, steps=10000)
        V.append(v)

    V_val = np.array(V)

    V_val = np.expand_dims(V_val, axis=0)
    if verbose == True: print("Vectors generated!")
    return V_val

def select_next_phrase(model, V_val, candidate_list, verbose=False):
    sims_list = []
    preds = model.predict(V_val, verbose=0)[0]

    for candidate in candidate_list:
        V = np.array(d2v_model.infer_vector(candidate[1]))
        sim = scipy.spatial.distance.cosine(V, preds)
        sims_list.append(sim)

    m = max(sims_list)
    index_max = sims_list.index(m)

    if verbose == True:
        print("selected phrase:")
        print("   ", candidate_list[index_max][0])

    return candidate_list[index_max]

def generate_paragraph(phrase_seed, sentences_seed,
                       max_words = 50,
                       nb_words_in_seq=20,
                       temperature=1,
                       nb_phrases=30,
                       nb_candidates_sents=10,
                       verbose=True):
    sentences_list = sentences_seed
    sentence = phrase_seed
    text = []

    for p in range(nb_phrases):
        if verbose == True : print("")
        if verbose == True : print("#############")
        print("phrase ", p+1, "/", nb_phrases)
        if verbose == True : print("#############")
        if verbose == True :
            print('Sentence to generate phrase : ')
            print("      ", sentence)
            print("")
            print('List of sentences to contrain next phrase : ')
            print("      ", sentences_list)
            print("")
        V_val = generate_training_vector(sentences_list, verbose = verbose)

        if verbose == True : print("generate phrases candidates....")
        phrases_candidates = define_phrases_candidates(sentence,
                                                       max_words=max_words,
                                                       nb_words_in_seq=nb_words_in_seq,
                                                       temperature=temperature,
                                                       nb_candidates_sents=nb_candidates_sents,
                                                       verbose = verbose)
        if verbose == True: print("select next phrase...")
        next_phrase = select_next_phrase(model_sequence,
                                         V_val,
                                         phrases_candidates,
                                         verbose=verbose)

        print("Next phrase: ", next_phrase[0])
        if verbose == True:
            print("")
            print("Shift phrases in sentences list...")
        for i in range(len(sentences_list)-1):
            sentences_list[i]=sentences_list[i+1]

        sentences_list[len(sentences_list)-1] = next_phrase[0]

        if verbose == True:
            print('done.')
            print("New list of sentences: ")
            print("        ", sentences_list)
        sentence = next_phrase[1]
        text.append(next_phrase[0])
    return text

s1 = "talk to the sea lady ."
s2 = "message the sea lady ."
s3 = "ask about the book :"
s4 = "and as for mr. bunting , he was in bed ."
s5 = "i've never tasted tea before ."
s6 = "what a strange -- what a wonderful world it must be !"
s7 = "and she filled the cup ."
s8 = "how wonderful it must be to see the fishes !"
s9 = "it's not an occasion for sticking at trifles ."
s10 = "a sort of subdued cheer went around from the assembled buntings ."
s11 = "what have we been talking about ?"
s12 = "you are too much -- the agent general of his duty ."
s13 = "there are better dreams !"
s14 = "death said melville starkly , and for a space both stood without a word ."
s15 = "he found himself walking ."


sentences_list = [s1, s2, s3,s4,s5,s6,s7,s8,s9,s10,s11,s12,s13,s14,s15]
phrase_seed, sentences_seed = create_seed(s1 + " " + s2 + " " +
                                          s3 + " " + s4+ " " + s5 + " " +
                                          s6 + " " + s7 + " " + s8 + " " +
                                          s9+ " " + s10 + " " + s11 + " " +
                                          s12 + " " + s13 + " " + s14+ " " + s15, 20)

text = generate_paragraph(sentences_seed, sentences_list,
                          max_words = 100,
                          nb_words_in_seq = 30,
                          temperature = 0.2,
                          nb_phrases = 2,
                          nb_candidates_sents = 15,
                          verbose = True)
print("generated text: ")
for t in text:
    print(t)





