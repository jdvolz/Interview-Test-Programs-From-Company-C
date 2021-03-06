using System;
using System.Collections.Generic;
using System.IO;

namespace Causes 
{
public class Causes
{

	public static void Main(string[] args)
	{
		DateTime start = DateTime.Now;
		Console.WriteLine(start);
		string root = "causes";
		Dictionary<string,int> hash = new Dictionary<string,int>();
		List<string> queue = new List<string>();

		Dictionary<string, int> allWords = new Dictionary<string, int>();
		string[] sources = File.ReadAllLines("source");
		foreach(string word in sources) {allWords[word] = 0;}

		queue = Remove(root, allWords, hash);
		queue.AddRange(Add(root, allWords, hash));
		queue.AddRange(Switch(root, allWords, hash));
		hash["causes"]=0;
		while(queue.Count != 0)
		{
			string word = queue[0];
			queue.RemoveAt(0);
			if( hash.ContainsKey(word)) continue;
			hash[word]=0;
			queue.AddRange(Remove(word, allWords, hash));
			queue.AddRange(Add(word, allWords, hash));
			queue.AddRange(Switch(word, allWords, hash));
		}

		string[] keys = new string[hash.Keys.Count];		
		hash.Keys.CopyTo(keys, 0);
		File.AppendAllText("out", String.Join("\n", keys));

		DateTime end = DateTime.Now;
		Console.WriteLine(end);
		Console.WriteLine(end-start);
	}

	public static List<string> Remove(string s, Dictionary<string, int> allWords, Dictionary<string,int> viewedWords)
	{
		List<string> results = new List<string>();
		for(int i = 0; i < s.Length; i++)
		{
			string temp = s.Substring(0,i)+s.Substring(i+1, s.Length-i-1);
			if ( allWords.ContainsKey(temp) && !viewedWords.ContainsKey(temp))
							results.Add(temp);
		}
		return results;
	}
	public static List<string> Add(string s, Dictionary<string, int> allWords, Dictionary<string,int> viewedWords)
	{
		List<string> results = new List<string>();
		string alpha = "abcdefghijklmnopqrstuvwxyz";
		for(int i = 0; i <= s.Length; i++)
		{
			for(int j = 0; j < alpha.Length; j++)
			{
				string temp = s.Substring(0,i) + alpha[j];
				if (i < s.Length)
					temp += s.Substring(i,s.Length-i);
				if( allWords.ContainsKey(temp) && !viewedWords.ContainsKey(temp))
					results.Add(temp);
			}
		}
		return results;
	}
	public static List<string> Switch(string s, Dictionary<string, int> allWords, Dictionary<string,int> viewedWords)
	{
		List<string> results = new List<string>();
		string alpha = "abcdefghijklmnopqrstuvwxyz";
		for(int i = 0; i < s.Length; i++)
		{
			for(int j = 0; j < alpha.Length; j++)
			{
				string temp = s;
				temp = temp.Remove(i,1).Insert(i, alpha[j].ToString());
				if( temp.CompareTo(s) != 0 && allWords.ContainsKey(temp) && !viewedWords.ContainsKey(temp))
					results.Add(temp);
			}
		}
		return results;
	}
}
}
