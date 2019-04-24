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
from keras.models import load_model
import numpy as np
import argparse
from six.moves import cPickle
import codecs
import time
import sys
import spacy
nlp = spacy.load('en_core_web_sm')

data_dir = 'S:\CrispyPancake\data'
save_dir = os.path.join('S:\CrispyPancake', "save")
vocab_file = os.path.join(save_dir, "words_vocab.pk1")
file_list = ["105"]
sequences_step = 1
seq_length = 30

with open(os.path.join(save_dir, 'words_vocab.pk1'), 'rb') as f:
    words, vocab, vocabulary_inv = cPickle.load(f)

vocab_size = len(words)

print("loading model...")
model = load_model(save_dir + "/" + 'my_model_generate_sentences.h5')

def sample(preds, temperature=0.33):
    preds = np.asarray(preds).astype('float64')
    preds = np.log(preds) / temperature
    exp_preds = np.exp(preds)
    preds = exp_preds / np.sum(exp_preds)
    probas = np.random.multinomial(1, preds, 1)
    return np.argmax(probas)

words_number = 30
seed_sentences = "the sea lady walks ."

generated = ''
sentence = []

for i in range (seq_length):
    sentence.append("a")

seed = seed_sentences.split()

for i in range(len(seed)):
    sentence[seq_length-i-1]=seed[len(seed)-i-1]

    generated += ' '.join(sentence)

for i in range(words_number):
    x = np.zeros((1, seq_length, vocab_size))
    for t, word in enumerate(sentence):
        x[0, t, vocab[word]] = 1

    preds = model.predict(x, verbose=0)[0]
    next_index = sample(preds, 0.33)
    next_word = vocabulary_inv[next_index]

    generated += " " + next_word

    sentence = sentence[1:] + [next_word]

print(generated)
