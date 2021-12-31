using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NetSpell.SpellChecker;

namespace SpellingBeeWindow
{
    public partial class SpellingBee : Form
    {
        private List<string> list;
        private List<string> currentWord;
        private List<string> usedWords;

        private Spelling spellChecker;

        private string yellowLetter;

        public SpellingBee(List<string> listOfLetters, string mainLetter, string wordToCompareTo)
        {
            InitializeComponent();
            list = listOfLetters;
            yellowLetter = mainLetter;
            label1.Font = new Font(Label.DefaultFont, FontStyle.Bold);
            currentWord = new List<string> {wordToCompareTo};
            usedWords = new List<string>();
            spellChecker = new Spelling();
            spellChecker.ShowDialog = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Text = list[0];
            button2.Text = list[1];
            button3.Text = list[2];
            button4.Text = list[3];
            button5.Text = list[4];
            button6.Text = list[5];
            button7.Text = yellowLetter; //Never change the yellow button (button7)
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (label1.Text.Length < 7)
            {
                label1.Text += yellowLetter;
            }
        }

        private void EnterButton_Click(object sender, EventArgs e)
        {
            if (label1.Text.Length != 0 && label1.Text.Length > 2)
            {
                if (!usedWords.Contains(label1.Text))
                {
                    if (spellChecker.TestWord(label1.Text))
                    {
                        currentWord.Add(label1.Text);
                        foreach (var list in FindAllAnagramsWithDuplicateLetters(currentWord))
                        {
                            foreach (var word in list)
                            {
                                textBox1.AppendText(word);
                                textBox1.AppendText(Environment.NewLine);
                            }
                        }

                        usedWords.Add(label1.Text);
                        currentWord.Remove(label1.Text);
                        label1.Text = ""; //reset label
                    }
                    else
                    {
                        label1.Text = ""; //reset label
                    }
                }
            }
        }

        private void ShuffleButton_Click(object sender, EventArgs e)
        {
            var random = new Random();
            var shuffledList = list.OrderBy(letter => random.Next());
            list = new List<string>();
            foreach (var letter in shuffledList)
            {
                list.Add(letter);
            }

            button1.Text = list[0];
            button2.Text = list[1];
            button3.Text = list[2];
            button4.Text = list[3];
            button5.Text = list[4];
            button6.Text = list[5];
            //Never change the yellow button (button7)
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                label1.Text = label1.Text.Remove(label1.Text.Length - 1, 1);
            }
            catch (Exception exception)
            {
                Console.WriteLine("No characters to erase!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (label1.Text.Length < 7)
            {
                label1.Text += button1.Text;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (label1.Text.Length < 7)
            {
                label1.Text += button2.Text;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (label1.Text.Length < 7)
            {
                label1.Text += button3.Text;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (label1.Text.Length < 7)
            {
                label1.Text += button4.Text;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (label1.Text.Length < 7)
            {
                label1.Text += button5.Text;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (label1.Text.Length < 7)
            {
                label1.Text += button6.Text;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e) { }

        public static List<List<String>> FindAllAnagramsWithDuplicateLetters(List<String> wordsList)//Taken from previous HW
        {
            if (wordsList == null)
            {
                throw new ArgumentException("Param 'wordList' is null or empty");
            }

            if (wordsList.Contains(null) || wordsList.Contains(""))
            {
                throw new ArgumentException("Param 'wordList' contains null or empty val");
            }

            List<List<string>> listOfAnagrams = new List<List<string>>();

            if (wordsList.Count == 1)
            {
                return listOfAnagrams;
            }

            var currentAnagrams = new List<String>();

            foreach (var currentWord in wordsList)
            {
                foreach (var possibleAnagram in wordsList)
                {
                    var lowerAndTrim1 = currentWord.ToLower().Trim().ToCharArray();
                    var lowerAndTrim2 = possibleAnagram.ToLower().Trim().ToCharArray();

                    Array.Sort(lowerAndTrim1);
                    Array.Sort(lowerAndTrim2);

                    var s1 = new string(lowerAndTrim1);
                    var s2 = new string(lowerAndTrim2);

                    if (s1 == s2 && !currentAnagrams.Contains(possibleAnagram) && possibleAnagram != currentWord)
                    {
                        currentAnagrams.Add(possibleAnagram);
                    }

                    bool hasSameLetters = true;

                    ArrayList characterCount1 = new ArrayList();

                    foreach (char letter in lowerAndTrim1)
                    {
                        characterCount1.Add((int) letter);
                    }

                    foreach (char letter in lowerAndTrim2)
                    {
                        if (!characterCount1.Contains((int) letter))
                        {
                            hasSameLetters = false;
                        }
                    }

                    if (hasSameLetters && !currentAnagrams.Contains(possibleAnagram) && possibleAnagram != currentWord)
                    {
                        currentAnagrams.Add(possibleAnagram);
                    }
                }
            }

            currentAnagrams.Sort();
            if (currentAnagrams.Count != 0)
            {
                listOfAnagrams.Add(currentAnagrams);
            }

            return listOfAnagrams;
        }
    }
}
