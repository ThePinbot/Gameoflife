using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace V1
{
  public partial class LifeForm : Form
  {
    #region CONSTANTS
    const int MAX_CELLS = 30;
    const int CELL_SIZE = 20;
    const int C_M = 2; // CellMargin
        List<Point>_safeList = new List<Point>();
        #endregion
    
       
    // 2 Matritzen zur Verwaltung der n'ten und der n+1'ten Generation
    bool[,] _CA = new bool[MAX_CELLS, MAX_CELLS];
    bool[,] _CB = new bool[MAX_CELLS, MAX_CELLS];
    bool[,] _CC; // current ( active ) CellArray n'te Generation
        
    public LifeForm()
    {
      InitializeComponent();
      timer1.Interval = 100;
      // zum Testen ein paar Zellen setzen
       //_CA[3, 3] = true; _CA[3, 4] = true; _CA[3, 5] = true;
       //_CA[4, 3] = true; _CA[4, 4] = true; _CA[4, 5] = true;
      _CC = _CA;
    }

    private void OnPanelPaint(object sender, PaintEventArgs e)
    {
      DrawGrid(e.Graphics);
      DrawCells(e.Graphics);
    }

    private void OnPanelMouseDown(object sender, MouseEventArgs e)
    {
      // der Cell Editor
      TurnCellOnOff( e.X/CELL_SIZE, e.Y/CELL_SIZE);
      m_panel.Invalidate();
    }

    // Cells des aktiven Arrays (_CC) zeichnen
    void DrawCells(Graphics gr)
    {
            Brush _br = Brushes.Blue;

            for (int i = 0; i < MAX_CELLS; i++)
                for (int j = 0; j < MAX_CELLS; j++)
                {
                    if (_CC[i, j])
                        gr.FillRectangle(_br, i * CELL_SIZE , j * CELL_SIZE , (CELL_SIZE ), (CELL_SIZE));


                }

        }

        void DrawGrid(Graphics gr)
    {
            // Raster zeichnen
            Pen _pen = Pens.Red;

            // <= damit das Feld einen Abschlussrahmen sein
            for (int i = 0; i <= MAX_CELLS; i++)
            {
                gr.DrawLine(_pen, 0, i * CELL_SIZE, MAX_CELLS * CELL_SIZE, i * CELL_SIZE);//Wagrecht

            }

            for (int i = 0; i <= MAX_CELLS; i++)
            {
                gr.DrawLine(_pen, i * CELL_SIZE, 0, i * CELL_SIZE, MAX_CELLS * CELL_SIZE);//senkrecht

            }
        }

    // Nächste Generation berechnen
    private void OnStepButton(object sender, EventArgs e)
    {

      if (_CC == _CA)
      {
        ClearCells(_CB);
        CalcNextGeneration(_CA, _CB);
        _CC = _CB;
    
      }
      else
      {
        ClearCells(_CA);
        CalcNextGeneration(_CB, _CA);
        _CC = _CA;
    
            }
    
      m_panel.Invalidate();
    }

    private void OnTimerChk(object sender, EventArgs e)
    {
      if (timer1.Enabled)
        timer1.Enabled = false;
      else
        timer1.Enabled = true;
    }

    private void OnTimer(object sender, EventArgs e)
    {
      OnStepButton(null, null);
    }

    private void OnClearButton(object sender, EventArgs e)
    {
      ClearCells(_CA); ClearCells(_CB);
      m_panel.Invalidate();
    }



        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void safeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StreamWriter wr = new StreamWriter("file.txt");

            for (int i = 0; i <_CA.Length-1; i++)
            {
                for (int j = 0; i < _CA.Length-1; j++)
                {
                    if (_CC[i, j])
                    {
                        wr.WriteLine(i+ "," + j);
       

                    }
                }
            }
            wr.Close();
            MessageBox.Show("Bild wurde gespeichert");
        }


    
           
  
       


        
    }
}

