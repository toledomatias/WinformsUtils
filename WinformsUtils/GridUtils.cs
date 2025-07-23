using System.Reflection;

namespace WinformsUtils
{
    public static class GridUtils
    {
        public enum Align
        {
            Left,
            Center,
            Right
        }

        public class ColumnConfiguration
        {
            public bool Visible { get; set; }
            public string Name { get; set; }
            public string Title { get; set; }
            public int Width { get; set; }
            public Align Align { get; set; }
            public string Format { get; set; }
            public bool Fill { get; set; }
        }

        public class GridButton
        {
            public Button Button { get; set; }
            public Func<DataGridView, bool> OnSelectionChanged { get; set; }

            public GridButton(Button boton, Func<DataGridView, bool> onClick = null)
            {
                Button = boton;
                OnSelectionChanged = onClick;
            }
        }

        public class ColDefs
        {
            private readonly List<ColumnConfiguration> _columns = new List<ColumnConfiguration>();

            public static implicit operator List<ColumnConfiguration>(ColDefs def) => def._columns;
            public static ColDefs New() => new ColDefs();

            public ColDefs String(string name, string title = null)
            {
                _columns.Add(new ColumnConfiguration
                {
                    Visible = true,
                    Name = name,
                    Title = title ?? name,
                    Width = 20,
                    Align = Align.Left
                });
                return this;
            }

            public ColDefs Integer(string name, string title = null)
            {
                _columns.Add(new ColumnConfiguration
                {
                    Visible = true,
                    Name = name,
                    Title = title ?? name,
                    Width = 7,
                    Align = Align.Right,
                    Format = "N0"
                });
                return this;
            }

            public ColDefs Decimal(string name, string title = null)
            {
                _columns.Add(new ColumnConfiguration
                {
                    Visible = true,
                    Name = name,
                    Title = title ?? name,
                    Width = 11,
                    Align = Align.Right,
                    Format = "N2"
                });
                return this;
            }

            public ColDefs DateTime(string name, string title = null)
            {
                _columns.Add(new ColumnConfiguration
                {
                    Visible = true,
                    Name = name,
                    Title = title ?? name,
                    Width = 16,
                    Align = Align.Center,
                    Format = "dd/MM/yyyy HH:mm"
                });
                return this;
            }

            public ColDefs Date(string name, string title = null)
            {
                _columns.Add(new ColumnConfiguration
                {
                    Visible = true,
                    Name = name,
                    Title = title ?? name,
                    Width = 10,
                    Align = Align.Center,
                    Format = "dd/MM/yyyy"
                });
                return this;
            }

            public ColDefs Hidden(string name)
            {
                _columns.Add(new ColumnConfiguration
                {
                    Name = name,
                    Visible = false
                });
                return this;
            }

            public ColDefs Width(int width)
            {
                _columns.Last().Width = width;
                return this;
            }

            public ColDefs Fill()
            {
                _columns.Last().Fill = true;
                return this;
            }

            public ColDefs Center()
            {
                _columns.Last().Align = Align.Center;
                return this;
            }

            public ColDefs Right()
            {
                _columns.Last().Align = Align.Right;
                return this;
            }

            public List<ColumnConfiguration> Listo() => _columns;
        }

        public static void Configure<T>(this DataGridView gr, List<ColumnConfiguration> columns,
            IEnumerable<GridButton> buttons = null, Action<DataGridViewCellFormattingEventArgs> cellFormatter = null)
        {
            var gridBackground = Color.FromArgb(67, 71, 124);
            var gridColor = Color.FromArgb(240, 240, 240);
            var cellBackground = Color.FromArgb(176, 255, 176);

            gr.AutoGenerateColumns = true;
            gr.DoubleBuffered(true);
            gr.RowTemplate.Height = 26;

            gr.BackgroundColor = gridBackground;
            gr.GridColor = gridColor;

            var font = new Font("Microsoft Sans Serif", 9.75f);
            gr.Font = font;
            gr.ColumnHeadersDefaultCellStyle.Font = font;
            gr.EnableHeadersVisualStyles = true;
            gr.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
            gr.RowHeadersVisible = false;

            gr.AllowUserToResizeRows = false;
            gr.ReadOnly = true;

            gr.DataBindingComplete += (s, ev) =>
            {
                for (int i = 0; i < gr.Columns.Count; i++)
                {
                    if (!columns.Select(c => c.Name).Contains(gr.Columns[i].Name))
                    {
                        gr.Columns[i].Visible = false;
                    }
                }

                for (int i = 0; i < columns.Count; i++)
                {
                    var col = columns[i];
                    var columna = gr.Columns[col.Name];
                    if (col.Visible)
                    {
                        columna.HeaderText = col.Title;
                        columna.DisplayIndex = i;

                        switch (col.Align)
                        {
                            case Align.Center:
                                columna.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                break;
                            case Align.Right:
                                columna.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                                break;
                        }

                        columna.Width = col.Width * 10;
                        columna.DefaultCellStyle.Format = col.Format;
                        if (col.Fill)
                        {
                            columna.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        }
                    }
                    else
                    {
                        gr.HideColumn(col.Name);
                    }
                }

                gr.AutoGenerateColumns = false;
            };

            if (buttons != null)
            {
                gr.SelectionChanged += (s, e) =>
                {
                    var haySeleccion = gr.SelectedRows.Count > 0 || gr.CurrentRow != null;

                    foreach (var bg in buttons)
                    {
                        if (bg.OnSelectionChanged != null)
                        {
                            if (haySeleccion)
                            {
                                bg.Button.Enabled = bg.OnSelectionChanged(gr);
                            }
                        }
                        else
                        {
                            bg.Button.Enabled = haySeleccion;
                        }
                    }
                };
            }

            gr.CellFormatting += (s, e) =>
            {
                if (cellFormatter != null)
                {
                    cellFormatter?.Invoke(e);
                }
                else
                {
                    e.CellStyle.BackColor = cellBackground;
                }
            };

            gr.DataSource = new List<T>();
        }

        public static void HideColumn(this DataGridView gr, string name)
        {
            gr.Columns[name].Visible = false;
        }

        public static void DoubleBuffered(this DataGridView dgv, bool setting)
        {
            Type dgvType = dgv.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered",
                BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(dgv, setting, null);
        }
    }
}
