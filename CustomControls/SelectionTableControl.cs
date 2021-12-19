using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

// ReSharper disable UnusedMember.Global

namespace Util.CustomControls
{
    // ReSharper disable once UnusedMember.Global
    public class SelectionTableControl : TableLayoutPanel
    {
        private TableLayoutPanelCellPosition _selectedCell;
        private int _selectionWidth;

        public event EventHandler<PanelChangedEventArgs> SelectedPanelChanged;

        public SelectionTableControl()
        {
            _selectedCell = new TableLayoutPanelCellPosition(-1, -1);

            _selectionWidth = 3;
        }

        [Browsable(true)]
        public TableLayoutPanelCellPosition SelectedCell
        {
            get => _selectedCell;

            set
            {
                if (_selectedCell == value)
                    return;

                _selectedCell = value;
                Refresh();
            }
        }

        public int SelectorLineWidth
        {
            get => _selectionWidth;
            set
            {
                // // Contract.Requires(value >= 0 && value < 256);
                _selectionWidth = value;

                Refresh();
            }
        }

        public Color SelectionColor { get; set; } = Color.Black;

        protected override void OnCellPaint(TableLayoutCellPaintEventArgs e)
        {
            // Contract.Assume(e != null);

            base.OnCellPaint(e);

            Graphics g = CreateGraphics();
            if (_selectedCell != new TableLayoutPanelCellPosition(e.Column, e.Row)) return;
            using (Pen p = new Pen(SelectionColor, _selectionWidth))
            {
                g.Clear(BackColor);
                Rectangle cellRect = e.CellBounds;
                cellRect.X += _selectionWidth / 2;
                cellRect.Y += _selectionWidth / 2;
                cellRect.Width -= _selectionWidth;
                cellRect.Height -= _selectionWidth;
                g.DrawRectangle(p, cellRect);
            }
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            // Contract.Assume(e != null);

            base.OnControlAdded(e);

            // Contract.Assume(e.Control != null);
            e.Control.Margin = new Padding(_selectionWidth);
            e.Control.MouseClick += Control_MouseClick;                     
        }



        protected override void OnControlRemoved(ControlEventArgs e)
        {
            // Contract.Assume(e != null);

            base.OnControlRemoved(e);

            // Contract.Assume(e.Control != null);
            e.Control.MouseClick -= Control_MouseClick;                    
        }

        private void Control_MouseClick(object sender, MouseEventArgs e)
        {
            if (!(sender is Control ctrl)) return;
            var newCell = GetCellPosition(ctrl);

            if (newCell == _selectedCell) return;
            _selectedCell = newCell;

            OnSelectedPanelChanged(new PanelChangedEventArgs(_selectedCell));
        }

        protected virtual void OnSelectedPanelChanged(PanelChangedEventArgs e)
        {
            Refresh();

            SelectedPanelChanged?.Invoke(this, e);
        }

    }

    public class PanelChangedEventArgs : EventArgs
    {
        public PanelChangedEventArgs(TableLayoutPanelCellPosition cell)
        {
            Cell = cell;
        }

        public TableLayoutPanelCellPosition Cell { get; }
    }
}