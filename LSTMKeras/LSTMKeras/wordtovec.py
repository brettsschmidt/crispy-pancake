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


