from __future__ import print_function
import collections
import os
import tensorflow as tf
import keras as keras
from keras.models import Sequential, load_model, Model
from keras.layers import Dense, Activation, Embedding, Dropout, TimeDistributed
from keras.layers import Input, Dense, Bidirectional
from keras.layers import LSTM
from keras.optimizers import Adam
from keras.utils import to_categorical
from keras.callbacks import ModelCheckpoint, EarlyStopping
from keras.metrics import categorical_accuracy
import numpy as np
import argparse
from six.moves import cPickle
import codecs
import time
import sys
import spacy
nlp = spacy.load('en_core_web_sm')
import gensim
from gensim.models.doc2vec import LabeledSentence

data_dir = 'S:\CrispyPancake\data'
save_dir = os.path.join('S:\CrispyPancake', "save")
vocab_file = os.path.join(save_dir, "words_vocab.pk1")
file_list = ["105"]
sequences_step = 1

sentences = []
sentences_label = []

def create_sentences(doc):
    punctuation = [".","?","!",":","…"]
    sentences = []
    sent = []
    for word in doc:
        if word.text not in punctuation:
            if word.text not in ("\n","\n\n",'\u2009','\xa0'):
                sent.append(word.text.lower())
        else:
            sent.append(word.text.lower())
            if len(sent) > 1:
                sentences.append(sent)
            sent = []
    return sentences

for file_name in file_list:
    input_file = os.path.join(data_dir, file_name + ".txt")
    with codecs.open(input_file, "r") as f:
        data = f.read()
    doc = nlp(data)
    sents = create_sentences(doc)
    sentences = sentences + sents

for i in range(np.array(sentences).shape[0]):
    sentences_label.append("ID" + str(i))

class LabeledLineSentence(object):
    def __init__(self, doc_list, labels_list):
        self.labels_list = labels_list
        self.doc_list = doc_list
    def __iter__(self):
        for idx, doc in enumerate(self.doc_list):
            yield gensim.models.doc2vec.LabeledSentence(doc,[self.labels_list[idx]])

def train_doc2vec_model(data, docLabels, size=300, sample=0.000001, dm=0, hs=1, window=10, min_count=0, workers=8,alpha=0.024, min_alpha=0.024, epoch=15, save_file='doc2vec.w2v') :
    startime = time.time()
    print("{0} articles loaded for model".format(len(data)))

    it = LabeledLineSentence(data, docLabels)

    model = gensim.models.Doc2Vec(size=size, sample = sample, dm=dm, window = window, min_count=min_count, workers=workers,alpha=alpha, min_alpha=min_alpha, hs=hs)
    model.build_vocab(it)
    for epoch in range(epoch):
        print("Training epoch {}".format(epoch+1))
        model.train(it, total_examples=model.corpus_count, epochs=model.iter)
    model.save(os.path.join(data_dir, save_file))
    print('model saved')

train_doc2vec_model(sentences, sentences_label, size=500, sample=0.0, alpha=0.025, min_alpha=0.001, min_count=0, window=10, epoch=20, dm=0, hs=1, save_file='doc2vec.w2v')




d2v_model = gensim.models.doc2vec.Doc2Vec.load(os.path.join(data_dir, 'doc2vec.w2v'))

sentences_vector=[]

t = 500

for i in range(len(sentences)):
    if i % t == 0:
        print("sentence", i, ":", sentences[i])
        print("***")
    sent = sentences[i]
    sentences_vector.append(d2v_model.infer_vector(sent, alpha=0.001, min_alpha=0.001, steps=10000))

sentences_vector_file = os.path.join(save_dir, "sentences_vector_500_a001_ma001_s10000.pk1")

with open(os.path.join(sentences_vector_file), 'wb') as f:
    cPickle.dump((sentences_vector), f)

nb_sequenced_sentences = 15
vector_dim = 500

X_train = np.zeros((len(sentences), nb_sequenced_sentences, vector_dim), dtype=np.float)
y_train = np.zeros((len(sentences), vector_dim), dtype=np.float)

t = 1000
for i in range(len(sentences_label)-nb_sequenced_sentences-1):
    if i % t == 0: print("new sequence: ", i)

    for k in range(nb_sequenced_sentences):
        sent = sentences_label[i+k]
        vect = sentences_vector[i+k]

        if i % t == 0:
            print(" ", k + 1, "the vector for this sequence. Sentence ", sent, "(vector dim = ", len(vect), ")")

        for j in range(len(vect)):
            X_train[i, k, j] = vect[j]

    senty = sentences_label[i+nb_sequenced_sentences]
    vecty = sentences_vector[i + nb_sequenced_sentences]
    if i % t == 0: print(" y vector for this sequence ", senty, ": (vector dim = ", len(vecty), ")")
    for j in range(len(vecty)):
        y_train[i, j] = vecty[j]

print(X_train.shape, y_train.shape)

def bidirectional_lstm_model(seq_length, vector_dim):
    print('Biulding LSTM model...')
    model = Sequential()
    model.add(Bidirectional(LSTM(rnn_size, activation="relu"), input_shape=(seq_length, vector_dim)))
    model.add(Dropout(0.5))
    model.add(Dense(vector_dim))

    optimizer = Adam(lr=learning_rate)
    callbacks=[EarlyStopping(patience=2, monitor='val_loss')]
    model.compile(loss='logcosh', optimizer=optimizer, metrics=['acc'])
    print('LSTM model built.')
    return model

rnn_size = 32
vector_dim = 500
learning_rate = 0.0001

model_sequence = bidirectional_lstm_model(nb_sequenced_sentences, vector_dim)

batch_size = 30

callbacks=[EarlyStopping(patience=3, monitor='val_loss'),
           ModelCheckpoint(filepath=save_dir + "/" + 'my_model_sequence_lstm.{epoch:02d}.hdf5',     
                           monitor='val_loss', verbose=1, mode='auto', period=5)]

history = model_sequence.fit(X_train, y_train,
                             batch_size=batch_size,
                             shuffle=True,
                             epochs=40,
                             callbacks=callbacks,
                             validation_split=0.1)

model_sequence.save(save_dir + "/" + 'my_model_sequence_lstm.final.hdf5')