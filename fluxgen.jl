using Flux
using Flux: onehot, chunk, batchseq, throttle, crossentropy
using StatsBase: wsample
using Base.Iterators: partition
using BSON: @save

cd(@__DIR__)



text = collect(String(read("S:/CrispyPancake/data/wells.txt")))
alphabet = [unique(text)..., '_']
text = map(ch -> onehot(ch, alphabet), text)
stop = onehot('_', alphabet)

N = length(alphabet)
seqlen = 50
nbatch = 50

Xs = collect(partition(batchseq(chunk(text, nbatch), stop), seqlen))
Ys = collect(partition(batchseq(chunk(text[2:end], nbatch), stop), seqlen))

m = Chain(
  LSTM(N, 128),
  LSTM(128, 128),
  Dense(128, N),
  softmax)

m = gpu(m)

function loss(xs, ys)
  l = sum(crossentropy.(m.(gpu.(xs)), gpu.(ys)))
  Flux.truncate!(m)
  return l
end

opt = ADAM(0.01)
tx, ty = (Xs[5], Ys[5])
evalcb = () -> @show loss(tx, ty)

Flux.train!(loss, params(m), zip(Xs, Ys), opt,
            cb = throttle(evalcb, 30))

@save "S:/CrispyPancake/data/wellsmodel.bson" m

function GenVocab(filepath::String)
    s = open(filepath) do f
        read(f, String)

    end

    s = replace(s, "\r\n" => " ")
    s = replace(s, "\ufeff" => " ")
    s = replace(s, "=" => " ")
    s = replace(s, "---" => " ")
    s = replace(s, "." => " ")
    s = replace(s, "?" => " ")
    s = replace(s, "!" => " ")

    twords = split(s, " ", keepempty = false)
    twords = unique(twords)

    words = [[ch for ch in word] for word in twords]
end


function Matchword(query, words)
    aquery = [ch for ch in query]
    highscore = 0
    highindex = 1
    for (i, word) in enumerate(words)
        score = 0
        len = (length(word) > length(aquery)) ? length(aquery) : length(word)
        for j in 1:len
            score += ( word[j] == aquery[j]) ? 1 : 0
        end
        highindex = (highscore > score) ? highindex : i
        highscore = (highscore > score) ? highscore : score
    end
    word = ""
    punc = ""
    (in('.', words[highindex])) ? punc = "." : punc = punc
    (in('!', words[highindex])) ? punc = punc * "!" : punc = punc
    (in('?', words[highindex])) ? punc = punc * "?" : punc = punc
    for ch in words[highindex]
        word = word * string(ch)
    end
    word = word * punc
    return word
end

# Sampling

function sample(m, alphabet, len; temp = 1)
  m = cpu(m)
  Flux.reset!(m)
  buf = IOBuffer()
  c = rand(alphabet)
  for i = 1:len
    write(buf, c)
    c = wsample(alphabet, m(onehot(c, alphabet)).data)
  end
  return String(take!(buf))
end

function sample(m, alphabet, len, vocab; temp = 1)
  m = cpu(m)
  Flux.reset!(m)
  buf = IOBuffer()
  c = rand(alphabet)
  for i = 1:len
    write(buf, c)
    c = wsample(alphabet, m(onehot(c, alphabet)).data)
  end
  temp = split(String(take!(buf)), " ")
  words = [Matchword(word, vocab) for word in temp]
  samp = ""
  for word in words
      samp = samp * word * " "
  end
  return samp

end


# evalcb = function ()
#   @show loss(Xs[5], Ys[5])
#   println(sample(deepcopy(m), alphabet, 100))
# end
function Gendiv(paragraph::String, choicetext1::String, choicetext2::String, id, link1, link2)
    text = """<div class="inner" id="$id">
        <p>$paragraph</p>
            <table>
                <tr>
                    <td><a href="#$link1">$choicetext1</a></td>
                    <td><a href="#$link2">$choicetext2</a></td>
                </tr>
            </table>
        </p>
    </div>"""
end
function Gensection(paragraph::String, choicetext1::String, choicetext2::String, id, link1, link2, style)
    text = Gendiv(paragraph, choicetext1, choicetext2, id, link1, link2)
    text = """<section class="$style">$text</section>"""
end

function Style(choice::Int)
    if choice == 1
        style = "wrapper style1 fade-up"
    elseif choice == 2
        style = "wrapper style2 spotlights"
    else
        style = "wrapper style3 fade-up"
    end
    style
end

function Htmlbeginning(title::String)
    text = """<!DOCTYPE HTML>
    <!--
    Hyperspace by HTML5 UP
    html5up.net | @ajlkn
    Free for personal and commercial use under the CCA 3.0 license (html5up.net/license)
        -->
        <html>
        <head>
            <title>$title</title>
            <meta charset="utf-8" />
            <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no" />
            <link rel="stylesheet" href="assets/css/main.css" />
            <noscript><link rel="stylesheet" href="assets/css/noscript.css" /></noscript>
            </head>
            <body class>
    """
end

htmlmiddle = """<!-- Wrapper --> <div id="wrapper">"""
htmlend = """</div>
    <!-- Footer -->
			<footer id="footer" class="wrapper style1-alt">
				<div class="inner">
					<ul class="menu">
						<li>Created by Brett, Haley, Austin, and Corey</li>
						<li><a href="http://penguinsaffiliated.io">Penguins Affiliated</a></li>
						<li>Adapted from Hyperspace by HTML5 UP html/css template</li>
						<li>Built using Julia</li>
						<li><a href="LICENSE.txt">Creative Commons License</a></li>

					</ul>
					<img src="assets/css/images/Half Cangaroo B-01.png" style="width: 25%; height: 25%;">
				</div>
			</footer>

		<!-- Scripts -->
			<script src="assets/js/jquery.min.js"></script>
			<script src="assets/js/jquery.scrollex.min.js"></script>
			<script src="assets/js/jquery.scrolly.min.js"></script>
			<script src="assets/js/browser.min.js"></script>
			<script src="assets/js/breakpoints.min.js"></script>
			<script src="assets/js/util.js"></script>
			<script src="assets/js/main.js"></script>


	</body>
    </html>
    """




function Gendocument(m, alphabet, amtps, navbar, vocab)
    paras = [sample(m, alphabet, 1000, vocab) for n in 1:amtps]
    choice1s = [sample(m, alphabet, 250, vocab) for n in 1:amtps ]
    choice2s = [sample(m, alphabet, 250, vocab) for n in 1:amtps]
    html = Htmlbeginning("Spooky Story") * navbar * htmlmiddle


    for index in 1:amtps
        html = html * Gensection(paras[index], choice1s[index], choice2s[index], index, rand(1:amtps), rand(1:amtps), Style(index % 3))
    end
    html = html * htmlend
end


function Gennav(doccount::Int)
    html = """<section id="sidebar">
        <div class="inner"><nav><ul>
        <li><a href="/index.html">Home</a></li>
    """
    for i in 1:doccount
        html = html * """<li><a href="/$i.html">Story $i</a></li>"""
    end
    html = html * """</ul></nav></div></section>"""
end


navbar = Gennav(1000)
vocab = GenVocab("S:/CrispyPancake/data/wells.txt")
for i in 1:1000
    sample = Gendocument(m, alphabet, 250, navbar, vocab)
    open("S:/CrispyPancake/sites3/$i.html", "w") do f
        write(f, sample)
    end
end
