def remove(orig, all_words, viewed_words)  #returns the array of good words
	results = []
	(0..orig.length-1).each do |position|
		temp = orig[0,position]+orig[position+1,orig.length-position-1]
		results << temp if all_words.has_key?(temp) and not viewed_words.has_key?(temp)
	end
	results
end

def add(orig, all_words, viewed_words) 
	results = []
	(0..orig.length).each do |position|
		('a'..'z').each do |letter| 
			temp = orig[0,position] + letter + orig[position,orig.length-position]
			results << temp if all_words.has_key?(temp) and not viewed_words.has_key?(temp)
		end
	end
	results
end

def switch(orig, all_words, viewed_words)
	results = []
	(0..orig.length-1).each do |position|
		('a'..'z').each do |letter|
			temp = orig.dup
			temp[position]=letter
			results << temp if (temp != orig and all_words.has_key?(temp) and not viewed_words.has_key?(temp))
		end
	end
	results
end

start = Time.now
puts start
root = 'causes'
hash = {}
queue = []
all_words = {}
File.open('source','r').read.split("\n").each {|x| all_words[x] = ''}
queue = remove(root, all_words, hash)
queue = queue + add(root, all_words, hash)
queue = queue + switch(root, all_words, hash)
hash['causes']=''
while(not queue.empty?)
	word = queue.shift
	next if hash.has_key?(word)
	hash[word] = ''
	queue = queue + remove(word, all_words, hash)
	queue = queue + add(word, all_words, hash)
	queue = queue + switch(word, all_words, hash)
end

File.open('out','w') do |f|
	hash.keys.each {|x| f.puts x}
end


time_end = Time.now
puts time_end
puts time_end - start
