using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.IO;
using System.Text.RegularExpressions;

namespace FoamingOJ
{

    public partial class SimulationScreen : Form
    {

        public SimulationScreen()
        {
            InitializeComponent();
            MessageBox.Show("Welcome to Business Snatch");
            #region Voice Synth
            SpeechSynthesizer synth = new SpeechSynthesizer();
            synth.SetOutputToDefaultAudioDevice();
            synth.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Adult);
            #endregion


            SimulationText_TXT.Text = "Welcome to Business Snatch!";
            SimulationText_TXT.Clear();
            synth.Speak($"{SimulationText_TXT.Text}");

            Random rnd = new Random();
            string picturePath = @"C://Users//Csanders41//Documents//GitHub//crispy-pancake//FoamingOJ//FoamingOJ//Images//";
            picture.Image = Image.FromFile(picturePath + rnd.Next(1, 3).ToString() + ".jpg");
            picture.SizeMode = PictureBoxSizeMode.StretchImage;



            #region Test
            #region Local Variable
            int start = 1;
            string readpath = @"C://Users//Csanders41//Documents//GitHub//crispy-pancake//FoamingOJ//FoamingOJ//TextFile//ReadText//WolfOfWallStreet.txt";
            string Startpath = $@"C://Users//Csanders41//Documents//GitHub//crispy-pancake//FoamingOJ//FoamingOJ//TextFile//{start}.txt";
            string NewTokenPath = $@"C://Users//Csanders41//Documents//GitHub//crispy-pancake//FoamingOJ//FoamingOJ//TextFile//{start = start + 1}.txt";
            string[] ptext;
            string tokentext;
            string[] token;
            string readtext;
            char[] sentenceEnd = { '.', '?', '!' };
            char[] newtoken = { '<', '>' };
            bool loop = true;
            int tokennumber = 0;

            #endregion
            StreamReader streamreader = new StreamReader(readpath);
            readtext = streamreader.ReadLine();

            ptext = readtext.Split(sentenceEnd);
            foreach (var sent in ptext)
            {

                File.AppendAllText(Startpath, $"<<<{Environment.NewLine}<<{tokennumber = tokennumber + 1}>>{Environment.NewLine} <{sent.ToString()}> {Environment.NewLine} >>>" + Environment.NewLine + Environment.NewLine);
                SimulationText_TXT.AppendText($"{sent.ToString() + Environment.NewLine + Environment.NewLine}");
            }
            streamreader.Close();
            StreamReader SplitStreamReader = new StreamReader(Startpath);
            tokentext = SplitStreamReader.ReadLine();
            token = tokentext.Split(newtoken);
            foreach (char line in tokentext)
            {

                File.AppendAllText(NewTokenPath, $"{line.ToString()}");
            }



            #endregion


        }


        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region skin changes
        private void lightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.DimGray;
            picture.BackColor = Color.GhostWhite;
            SimulationText_TXT.BackColor = Color.GhostWhite;
            SimulationText_TXT.ForeColor = Color.Black;
            OptionOne.BackColor = Color.GhostWhite;
            OptionOne.ForeColor = Color.Black;
            OptionTwo.BackColor = Color.GhostWhite;
            OptionTwo.ForeColor = Color.Black;
            OptionOneText_txt.BackColor = Color.GhostWhite;
            OptionOneText_txt.ForeColor = Color.Black;
            OptionTwoText_txt.BackColor = Color.GhostWhite;
            OptionTwoText_txt.ForeColor = Color.Black;
        }

        private void darkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.Black;
            picture.BackColor = Color.Purple;
            SimulationText_TXT.BackColor = Color.Purple;
            SimulationText_TXT.ForeColor = Color.White;
            OptionOne.BackColor = Color.Purple;
            OptionOne.ForeColor = Color.White;
            OptionTwo.BackColor = Color.Purple;
            OptionTwo.ForeColor = Color.White;
            OptionOneText_txt.BackColor = Color.Purple;
            OptionOneText_txt.ForeColor = Color.White;
            OptionTwoText_txt.BackColor = Color.Purple;
            OptionTwoText_txt.ForeColor = Color.White;

        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
            picture.BackColor = Color.LightGray;
            SimulationText_TXT.BackColor = Color.LightGray;
            SimulationText_TXT.ForeColor = Color.Black;
            OptionOne.BackColor = Color.LightGray;
            OptionOne.ForeColor = Color.Black;
            OptionTwo.BackColor = Color.LightGray;
            OptionTwo.ForeColor = Color.Black;
            OptionOneText_txt.BackColor = Color.LightGray;
            OptionOneText_txt.ForeColor = Color.Black;
            OptionTwoText_txt.BackColor = Color.LightGray;
            OptionTwoText_txt.ForeColor = Color.Black;

        }

        private void rainbowCardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.PaleVioletRed;
            picture.BackColor = Color.Blue;
            SimulationText_TXT.BackColor = Color.Yellow;
            SimulationText_TXT.ForeColor = Color.Black;
            OptionOne.BackColor = Color.Green;
            OptionOne.ForeColor = Color.Black;
            OptionTwo.BackColor = Color.Fuchsia;
            OptionTwo.ForeColor = Color.Black;
            OptionOneText_txt.BackColor = Color.Pink;
            OptionOneText_txt.ForeColor = Color.Black;
            OptionTwoText_txt.BackColor = Color.LightSeaGreen;
            OptionTwoText_txt.ForeColor = Color.Black;

        }

        private void peasInTheDarkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.Black;
            picture.BackColor = Color.ForestGreen;
            SimulationText_TXT.BackColor = Color.ForestGreen;
            SimulationText_TXT.ForeColor = Color.Black;
            OptionOne.BackColor = Color.ForestGreen;
            OptionOne.ForeColor = Color.Black;
            OptionTwo.BackColor = Color.ForestGreen;
            OptionTwo.ForeColor = Color.Black;
            OptionOneText_txt.BackColor = Color.ForestGreen;
            OptionOneText_txt.ForeColor = Color.Black;
            OptionTwoText_txt.BackColor = Color.ForestGreen;
            OptionTwoText_txt.ForeColor = Color.Black;

        }

        private void codeRedWhoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.Black;
            picture.BackColor = Color.DarkRed;
            SimulationText_TXT.BackColor = Color.DarkRed;
            SimulationText_TXT.ForeColor = Color.GhostWhite;
            OptionOne.BackColor = Color.DarkRed;
            OptionOne.ForeColor = Color.GhostWhite;
            OptionTwo.BackColor = Color.DarkRed;
            OptionTwo.ForeColor = Color.GhostWhite;
            OptionOneText_txt.BackColor = Color.DarkRed;
            OptionOneText_txt.ForeColor = Color.GhostWhite;
            OptionTwoText_txt.BackColor = Color.DarkRed;
            OptionTwoText_txt.ForeColor = Color.GhostWhite;


        }

        #endregion

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help hp = new Help();
            hp.ShowDialog();
        }

        private void OptionOne_Click(object sender, EventArgs e)
        {
            #region Voice Synth
            SpeechSynthesizer synth = new SpeechSynthesizer();
            synth.SetOutputToDefaultAudioDevice();
            synth.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Adult);
            synth.Speak($"{SimulationText_TXT.Text}");
            #endregion
            #region write Text
            string path = @"E://crispy-pancake//FoamingOJ//FoamingOJ//TextFile//1.txt";
            string[] save = { $"{SimulationText_TXT.Text}" };
            //File.AppendAllLines(path, save);

            Random rnd = new Random();
            string picturePath = @"E://crispy-pancake//FoamingOJ//FoamingOJ//Images//";
            picture.Image = Image.FromFile(picturePath + rnd.Next(1, 3).ToString() + ".jpg");
            picture.SizeMode = PictureBoxSizeMode.StretchImage;
            #endregion
        }

        private void OptionTwo_Click(object sender, EventArgs e)
        {
            #region Voice Synth
            SpeechSynthesizer synth = new SpeechSynthesizer();
            synth.SetOutputToDefaultAudioDevice();
            synth.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Adult);
            synth.Speak($"{SimulationText_TXT.Text}");
            #endregion

            #region write Text
            string path = @"E://crispy-pancake//FoamingOJ//FoamingOJ//TextFile//1.txt";
            string[] save = { $"{SimulationText_TXT.Text}" };
            //File.AppendAllLines(path, save);
            #endregion

            Random rnd = new Random();
            string picturePath = @"E://crispy-pancake//FoamingOJ//FoamingOJ//Images//";
            picture.Image = Image.FromFile(picturePath + rnd.Next(3).ToString() + ".jpg");
            picture.SizeMode = PictureBoxSizeMode.StretchImage;
            synth.Speak($"{SimulationText_TXT.Text}");
        }
    }
}
