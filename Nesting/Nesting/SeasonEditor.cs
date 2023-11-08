using Nesting.EditorLogic;
using Nesting.Panels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nesting
{
    public partial class SeasonEditor : Form
    {
        public UnknownPanel unk_panel = new UnknownPanel();
        public KnownPanel known_panel = new KnownPanel();
        public string main_folder;
        public SeasonEditor(string main_folder)
        {
            InitializeComponent();
            this.main_folder = main_folder;

            Controls.Add(unk_panel);
            Controls.Add(known_panel);
            shapeButton.Text = "Shape Folder \u27F6";

            unk_panel.Initialize(new ReferencesInjector(main_folder).GetReferences(), this);
            known_panel.Initialize(this);
            Draw();
        }

        public void MoveToSeason(Reference x, SeasonPanel season)
        {
            unk_panel.removeReference(x);
            foreach (SeasonPanel panel in known_panel.getSeasonList())
            {
                if (panel.getReferences().Contains(x)) panel.RemoveReference(x);
            }
            season.AddReference(x);
            Draw();
        }

        public void MoveToUnknownPanel(Reference x)
        {
            foreach (SeasonPanel season in known_panel.getSeasonList())
            {
                season.RemoveReference(x);
            }
            unk_panel.addReference(x);
            Draw();
        }

        private void Draw()
        {
            unk_panel.Draw();
            known_panel.Draw();
        }

        private void shapeButton_Click(object sender, EventArgs e)
        {
            if (unk_panel.getReferences().Count > 0)
            {
                string msg = "Unliked references will be moved to " + SeasonPanel.supplement_name + ". Are you sure this behaviour is intended?";
                DialogResult dialogResult = MessageBox.Show(msg, "Missing links", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.No)
                {
                    return;
                }
            }

            bool to_draw = false;
            if (known_panel.getSeasonList().Count == 0)
            {
                to_draw = true;
                known_panel.getSeasonList().Add(known_panel.addSeason());
            }
            for (int i= unk_panel.getReferences().Count-1; i>=0 ; i--)
            {
                to_draw = true;
                Reference x = unk_panel.getReferences()[i];
                MoveToSeason(x, known_panel.getSeasonList()[0]);
            }
            if (to_draw) Draw();

            string working_dir = Path.Combine(Path.GetDirectoryName(main_folder), "CyanVisionWorkingDirectory");
            string old_folder = Path.Combine(Path.GetDirectoryName(main_folder), "CyanVisionTrash_" + Path.GetFileName(main_folder));
            List<EditorLogic.Action> actions = new List<EditorLogic.Action>();
            actions.Add(new Create(working_dir));
            foreach (SeasonPanel panel in known_panel.getSeasonList()) actions.AddRange(panel.getMovements(working_dir));
            foreach(var file in Directory.GetFiles(main_folder))
            {
                if (Path.GetFileName(file) == "imagefromPowerVideos.jpg" || Path.GetFileName(file) == "infopowervideos.txt")
                {
                    actions.Add(new Copy(file, Path.Combine(working_dir, Path.GetFileName(file))));
                }
            }
            actions = orderActions(actions);
            actions.Add(new Create(main_folder, true));
            actions.Add(new Movement(working_dir, main_folder));
            if (ActionPlayer(actions))
            {
                log.Text = "Reshaping successful!";
            }
            else
            {
                log.Text = "Error while reshaping :(";
            }
        }

        private List<EditorLogic.Action> orderActions(List<EditorLogic.Action> actions)
        {
            List<EditorLogic.Action> ordered_actions = new List<EditorLogic.Action>();
            for (int i = 0; ; i++)
            {
                if (i >= actions.Count) break;
                if(actions[i].GetType().Name == "Create" && !((Create)actions[i]).rev_op) 
                {
                    ordered_actions.Add(actions[i]);
                    actions.RemoveAt(i);
                    i -= 1;
                }
            }
            for (int i = 0; ; i++)
            {
                if (i >= actions.Count) break;
                if (actions[i].GetType().Name == "Movement" && !((Movement)actions[i]).is_dir)
                {
                    ordered_actions.Add(actions[i]);
                    actions.RemoveAt(i);
                    i -= 1;
                }
            }
            for (int i = 0; ; i++)
            {
                if (i >= actions.Count) break;
                if (actions[i].GetType().Name == "Copy")
                {
                    ordered_actions.Add(actions[i]);
                    actions.RemoveAt(i);
                    i -= 1;
                }
            }
            for (int i = 0; ; i++)
            {
                if (i >= actions.Count) break;
                ordered_actions.Add(actions[i]);
                actions.RemoveAt(i);
                i -= 1;
            }
            return ordered_actions;
        }

        private bool ActionPlayer(List<EditorLogic.Action> actions)
        {
            int i = 0;
            try
            {
                for (i = 0; i < actions.Count; i++)
                {
                    bool performed = false;
                    actions[i].toString();
                    for (int j=0; j< 5; j++)
                    {
                        try
                        {
                            actions[i].PerformAction();
                            performed = true;
                            break;
                        }
                        catch { Thread.Sleep(500); }
                    }
                    if (!performed) { throw new Exception(); }
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message + "\nNow I am trying to revert all the changes!");
                Console.WriteLine("ERROR");
                i -= 1;
                try
                {
                    for (; i >= 0; i--)
                    {
                        actions[i].PerformAction(true);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\nSorry I messed up!!!");
                    return false;
                }
                return false;
            }
            return true;
        }
    }
}
