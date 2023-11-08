using Nesting.EditorLogic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Nesting
{
    public class SeasonPanel : Panel
    {
        private List<Reference> references;
        private Label seasonName;
        public int index = 0;
        public static string supplement_name = "Supplement";
        public SeasonPanel(int n) {
            references = new List<Reference>();
            BackColor = Color.FromArgb(10, 10, 10);
            Location = new Point(0, 0);
            Name = "s_panel" + n;
            AllowDrop = true;
            Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));

            seasonName = new Label();
            seasonName.ForeColor = Color.White;
            seasonName.BackColor = BackColor;
            seasonName.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            if (n == 0)
            {
                seasonName.Name = supplement_name.ToLower();
                seasonName.Text = supplement_name;
            }
            else
            {
                seasonName.Name = "s" + n;
                seasonName.Text = "Season " + n;
            }
            index = n;
            seasonName.TabIndex = 0;
            seasonName.TextAlign = ContentAlignment.MiddleCenter;
            Controls.Add(seasonName);
        }


        public List<Reference> getReferences()
        {
            return references;
        }
        public void AddReference(Reference reference)
        {
            reference.ShowTextbox(true);
            Console.WriteLine(reference.path + ": " + true);
            references.Add(reference);
        }
        public void RemoveReference(Reference reference)
        {
            references.Remove(reference);
        }

        public void Draw()
        {
            references = references.OrderBy(x => x.index).ToList();
            Controls.Clear();
            Controls.Add(seasonName);
            seasonName.Location = new Point(0, 0);
            int height = seasonName.Height;
            for (int i = 0; i < references.Count; i++)
            {
                references[i].Location = new Point(references[i].Location.X, height);
                Controls.Add(references[i]);
                height += references[i].Height + 5;
            }
            Size = new Size(860, height);
            seasonName.Size = new Size(860, 20);
        }

        public List<EditorLogic.Action> getMovements(string working_dir)
        {
            List<EditorLogic.Action> movements = new List<EditorLogic.Action>();
            string seasonFolder = Path.Combine(working_dir, seasonName.Text);
            movements.Add(new Create(seasonFolder));
            string index_text = index >= 10 ? index.ToString() : "0" + index.ToString();
            for (int i = 0; i < references.Count; i++)
            {
                Reference ref_ = references[i];
                string n_index_text = i >= 10 ? i.ToString() : "0" + (i+1).ToString();
                string new_dir = Path.Combine(seasonFolder, index_text + "x" + n_index_text);
                Create create_dir = new Create(new_dir);
                Movement move_movie = new Movement(ref_.path, Path.Combine(seasonFolder, new_dir, ref_.clean_name + ref_.extension));

                string[] directories = Directory.GetDirectories(ref_.directory);
                string[] files = Directory.GetFiles(ref_.directory);

                movements.Add(create_dir);
                movements.Add(move_movie);
                foreach (var file in files)
                {
                    if (!Program.IsVideo(file))
                    {
                        Copy copy_file = new Copy(file, Path.Combine(new_dir, Path.GetFileName(file)));
                        movements.Add(copy_file);
                    }
                }
                foreach (var dir in directories)
                {
                    Copy copy_folders = new Copy(dir, Path.Combine(new_dir, Path.GetFileName(dir)));
                    movements.Add(copy_folders);
                }
            }
            return movements;
        }
    }
}
