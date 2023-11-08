using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nesting.Panels
{
    public class UnknownPanel : Panel
    {
        private List<Reference> references = new List<Reference>();
        private SeasonEditor parent;
        public UnknownPanel()
        {
            AllowDrop = true;
            Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left))));
            BackColor = Color.Transparent;
            Location = new System.Drawing.Point(10, 10);
            Name = "unk_panel";
            Size = new System.Drawing.Size(600, 600);
            TabIndex = 3;
            AllowDrop = true;
            DragEnter += new DragEventHandler(dragEnter);
            DragDrop += new DragEventHandler(dragDrop);
            AutoScroll = true;
        }

        public void Initialize(List<Reference> in_references, SeasonEditor parent)
        {
            this.parent = parent;
            references.Clear();
            foreach (Reference reference in in_references)
            {
                reference.MouseDown += new MouseEventHandler(DropLoading);
                reference.getTextLabel().MouseDown += new MouseEventHandler(DropLoading);
                addReference(reference);
            }
            Draw();
            preLinkReferences();
            Draw();
        }
        private void preLinkReferences()
        {
            int last_season = 0;
            for (int j = references.Count - 1; j >= 0; j--)
            {
                Reference reference = references[j];
                string main = Path.GetDirectoryName(Path.GetDirectoryName(reference.directory));
                string season = Path.GetDirectoryName(reference.directory);
                int n = -1;
                try { n = Int32.Parse(Path.GetFileName(season).Substring(7)); } catch { }
                if (n > -1 && n < 20)
                {
                    if ((Path.GetFileName(season).Substring(0, 7) == "Season ")) { 
                        last_season = Math.Max(last_season, n);
                        for (int i = parent.known_panel.getSeasonList().Count; i <= last_season; i++)
                        {
                            parent.known_panel.addSeasonToList();
                        }
                        parent.MoveToSeason(reference, parent.known_panel.getSeasonList()[n]);
                    }
                }
            }
        }
        public List<Reference> getReferences()
        {
            return references;
        }
        public void addReference(Reference reference)
        {
            reference.ShowTextbox(false);
            Console.WriteLine(reference.path + ": " + false);
            references.Add(reference);
        }
        public void removeReference(Reference reference)
        {
            references.Remove(reference);
        }

        public void Draw()
        {
            references = references.OrderBy(x => x.index).ToList();
            Controls.Clear();
            int height = 10;
            for (int i = 0; i < references.Count; i++)
            {
                if (i != 0) height += references[i - 1].Height + 5;
                Controls.Add(references[i]);

                references[i].Location = new Point(references[i].Location.X, height);
                references[i].BringToFront();
            }
            if (references.Count > 0) height += references[references.Count - 1].Height + 5;
        }

        void DropLoading(object sender, MouseEventArgs e)
        {
            DoDragDrop((((Label)sender).Name), DragDropEffects.Copy);
        }

        void dragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text)) e.Effect = DragDropEffects.Copy;
        }
        void dragDrop(object sender, DragEventArgs e)
        {
            foreach (SeasonPanel ref_ in parent.known_panel.getSeasonList())
            {
                foreach (Reference x in ref_.getReferences())
                {
                    if (x.text.Name == (string)e.Data.GetData(DataFormats.Text))
                    {
                        parent.MoveToUnknownPanel(x);
                        return;
                    }
                }
            }
        }
    }
}
