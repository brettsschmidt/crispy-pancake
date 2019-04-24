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

print(np)


data_dir = 'S:\CrispyPancake\data'
save_dir = os.path.join('S:\CrispyPancake', "save")
vocab_file = os.path.join(save_dir, "words_vocab.pk1")
file_list = ["105"]
sequences_step = 1

def create_wordlist(doc):
    wl = []
    for word in doc:
        if word.text not in ("\n", "\n\n", '\u2009', '\xa0'):
            wl.append(word.text.lower())
    return wl


wordlist = []

for file_name in file_list:
    input_file = os.path.join(data_dir, file_name + ".txt")
    with codecs.open(input_file, "r", encoding='ISO-8859-1') as f:
        data = f.read()

    doc = nlp(data)
    wl = create_wordlist(doc)
    wordlist = wordlist + wl

word_counts = collections.Counter(wordlist)

vocabulary_inv = [x[0] for x in word_counts.most_common()]
vocabulary_inv = list(sorted(vocabulary_inv))

vocab = {x: i for i,  x in enumerate(vocabulary_inv)}
words = [x[0] for x in word_counts.most_common()]

vocab_size = len(words)
print("vocab size: ", vocab_size)

with open(os.path.join(vocab_file), 'wb') as f:
    cPickle.dump((words, vocab, vocabulary_inv), f)

seq_length = 30
sequences = []
next_words = []
for i in range(0, len(wordlist) - seq_length, sequences_step):
    sequences.append(wordlist[i: i + seq_length])
    next_words.append(wordlist[i + seq_length])



def bidirectional_lstm_model(seq_length, vocab_size):
    print('Build LSTM model.')
    model = Sequential()
    model.add(Bidirectional(LSTM(rnn_size, activation="relu"), input_shape=(seq_length, vocab_size)))
    model.add(Dropout(0.6))
    model.add(Dense(vocab_size))
    model.add(Activation('softmax'))

    optimizer = Adam(lr=learning_rate)
    callbacks=[EarlyStopping(patience=2, monitor='val_loss')]
    model.compile(loss='categorical_crossentropy', optimizer=optimizer, metrics=[categorical_accuracy])
    print("model built!")
    return model

rnn_size = 256
seq_length = 30
learning_rate = 0.001

md = bidirectional_lstm_model(seq_length, vocab_size)
md.summary()

batch_size = 32
num_epochs = 50
seq_count = len(sequences)

X = np.zeros((seq_count, seq_length, vocab_size), np.bool,)


y = np.zeros((seq_count, vocab_size), np.bool, 'C')
for i, sentence in enumerate(sequences):
    for t, word in enumerate(sentence):
        X[i, t, vocab[word]] = 1
    y[i, vocab[next_words[i]]] = 1

callbacks = [EarlyStopping(patience=4, monitor='val_loss'), 
             ModelCheckpoint(filepath=save_dir + "/" + 'my_model_gen_sentences.{epoch:02d}-{val_loss:.2f}.hdf5',
                             monitor='val_loss', verbose=0, mode='auto', period=2)]
history = md.fit(X, y,
                 batch_size = batch_size,
                 shuffle=True,
                 epochs=num_epochs,
                 callbacks=callbacks,
                 validation_split=0.1)
md.save(save_dir + "/" + 'my_model_gen_sentences.final.hdf5')




