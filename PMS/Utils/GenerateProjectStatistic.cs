using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using PMS.Model;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;


namespace PMS.Utils
{
    public class GenerateProjectStatistic
    {
        public void generateWordDocument (Project project)
        {

            Document doc = new Document();
            Section s = doc.AddSection();

            addTittle(s, project);
            addProjectName(s, project);
            addTeamName(s, project);

            Table costTable = s.AddTable(true);

            string[] Header = getHeaderCost();
            string[][] dataTable = getDataCost(project);
            costTable.ResetCells(dataTable.Length + 1, Header.Length);
            TableRow tRow = new TableRow(doc) { IsHeader = true};
            tRow = costTable.Rows[0];
            tRow.RowFormat.BackColor = Color.LightGray;
            headerFormat(tRow, Header);
            rowFormat(dataTable, costTable);

            addTotalCost(s, project);

            addMainTask(s, project);

            Table mainTaskTable = s.AddTable(true);

            string[] Header2 = getHeaderMainTask();
            string[][] dataTable2 = getDataMainTask(project);
            mainTaskTable.ResetCells(dataTable2.Length + 1, Header2.Length);
            TableRow tRow2 = new TableRow(doc) { IsHeader = true };
            tRow2 = mainTaskTable.Rows[0];
            tRow2.RowFormat.BackColor = Color.LightGray;
            headerFormat(tRow2, Header2);
            rowFormat2(dataTable2, mainTaskTable, project);

            addSubTask(s, project);


            Table subtaskTable = s.AddTable(true);
            string[] Header3 = getHeaderSubtask();
            string[][] dataTable3 = getDataSubtask(project);
            subtaskTable.ResetCells(dataTable3.Length + 1, Header3.Length);
            TableRow tRow3 = new TableRow(doc) { IsHeader = true };
            tRow3 = subtaskTable.Rows[0];
            tRow3.RowFormat.BackColor = Color.LightGray;
            headerFormat(tRow3, Header3);
            rowFormat(dataTable3, subtaskTable);


            saveAndLunch(doc, project);
        }

        #region costTable
        private void addProjectName (Section s, Project project)
        {
            Paragraph paragraphProjectName = s.AddParagraph();
            TextRange textProjectName = paragraphProjectName.AppendText("Project name: " + project.Name);
            textProjectName.CharacterFormat.FontName = "Calibri";
            textProjectName.CharacterFormat.FontSize = 11;
        }

        private void addMainTask(Section s, Project project)
        {
            Paragraph paragraphTeamName = s.AddParagraph();

            TextRange textTeamName = paragraphTeamName.AppendText("\nMain task: ");
            textTeamName.CharacterFormat.FontName = "Calibri";
            textTeamName.CharacterFormat.FontSize = 11;
            textTeamName.CharacterFormat.Bold = true;
        }

        private void addSubTask(Section s, Project project)
        {
            Paragraph paragraphTeamName = s.AddParagraph();

            TextRange textTeamName = paragraphTeamName.AppendText("\nSubtask: ");
            textTeamName.CharacterFormat.FontName = "Calibri";
            textTeamName.CharacterFormat.FontSize = 11;
            textTeamName.CharacterFormat.Bold = true;
        }

        private void addTeamName(Section s, Project project)
        {
            Paragraph paragraphTeamName = s.AddParagraph();

            TextRange textTeamName = paragraphTeamName.AppendText("Team name: " + project.Team.Name + "\n");
            textTeamName.CharacterFormat.FontName = "Calibri";
            textTeamName.CharacterFormat.FontSize = 11;
        }

        private void addTotalCost(Section s, Project project)
        {
            Paragraph paragraphTotalCost = s.AddParagraph();
            TextRange textTotalCost = paragraphTotalCost.AppendText("Total cost: " + project.Cost + "\n");
            textTotalCost.CharacterFormat.FontName = "Calibri";
            textTotalCost.CharacterFormat.FontSize = 11;
        }

        private void addTittle(Section s, Project project)
        {
            Paragraph paragraphTotalCost = s.AddParagraph();
            paragraphTotalCost.Format.HorizontalAlignment = HorizontalAlignment.Center;
            TextRange textTotalCost = paragraphTotalCost.AppendText("Project statistics\n");
            textTotalCost.CharacterFormat.FontName = "Calibri";
            textTotalCost.CharacterFormat.FontSize = 24;
            textTotalCost.CharacterFormat.Bold = true;
        }

        private string[] getHeaderCost() //resource and salary
        {
            string[] headerResource = {"Name", "Price", "Type"};
            return headerResource;
        }

        private void rowFormat(string[][] dataTable, Table costTable)
        {
            for (int r = 0; r < dataTable.Length; r++)
            {
                TableRow DataRow = costTable.Rows[r + 1];
                DataRow.Height = 19;

                for (int c = 0; c < dataTable[r].Length; c++) //c - column
                {
                    DataRow.Cells[c].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                    Paragraph par = DataRow.Cells[c].AddParagraph();
                    TextRange text = par.AppendText(dataTable[r][c]);
                    par.Format.HorizontalAlignment = HorizontalAlignment.Center;
                    text.CharacterFormat.FontName = "Calibri";
                    text.CharacterFormat.FontSize = 11;
                }
            }
        }


        private void headerFormat(TableRow tRow, string[] Header)
        {

            for (int i = 0; i < Header.Length; i++)
            {
                Paragraph par = tRow.Cells[i].AddParagraph();
                tRow.Cells[i].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                par.Format.HorizontalAlignment = HorizontalAlignment.Center;

                TextRange text = par.AppendText(Header[i]);
                text.CharacterFormat.TextColor = Color.Blue;
                text.CharacterFormat.FontSize = 15;
                text.CharacterFormat.FontName = "Times New Roman";
                text.CharacterFormat.Bold = true;
            }
        }



        private string[][] getDataCost(Project project) //resource and salary
        {
            int lenght = project.Resources.Count;
            string[][] data = new string[lenght+1][];
            int i = 0;

            double costResource = 0;

            foreach(var tmp in project.Resources)
            {
                
                costResource += tmp.Cost;
                string[] rowData = new string[3];
                rowData[0] = tmp.Name;
                rowData[1] = tmp.Cost.ToString();
                rowData[2] = "Resources";
                data[i] = rowData;
                i++;
            }

            double salary = project.Cost - costResource;
            
            string[] rowDataS = new string[3];
            rowDataS[0] = "Other";
            rowDataS[1] = salary.ToString();
            rowDataS[2] = "Salary";
            data[lenght] = rowDataS;

            return data;
        }

        #endregion

        public void saveAndLunch(Document doc, Project project) // zmienic nazwe dodania
        {
            string saveName = project.Name + DateTime.Now.ToString("MMddyyyyHHmm");
            doc.SaveToFile(saveName + ".docx", FileFormat.Docx2010);
            System.Diagnostics.Process.Start(saveName + ".docx");
        }




        private string[] getHeaderMainTask()
        {
            string[] headerMainTask = { "Name", "ES", "EF", "LS", "LF", "Eff.", "Pre. task" };
            return headerMainTask;
        }

        private string[][] getDataMainTask(Project project)
        {
            int lenght = project.MainTasks.Count;
            string[][] data = new string[lenght][];
            int i = 0;

            foreach (var tmp in project.MainTasks)
            {
                string[] rowData = new string[7];
                rowData[0] = tmp.Name;
                rowData[1] = tmp.EarlyStart.ToString("MM/dd/yyyy");
                rowData[2] = tmp.EarlyFinish.ToString("MM/dd/yyyy");
                rowData[3] = tmp.LateStart.ToString("MM/dd/yyyy");
                rowData[4] = tmp.LateFinish.ToString("MM/dd/yy");
                rowData[5] = tmp.Effort.ToString();
                rowData[6] = tmp.StringMainTask;
                data[i] = rowData;
                i++;
            }

            return data;
        }

        //private Color setColor(int i)
        //{
        //    Color color;

        //    return color;
        //}

        private void rowFormat2(string[][] dataTable, Table costTable, Project project)
        {
            List<MainTask> listMT = new List<MainTask>(project.MainTasks);

            for (int r = 0; r < dataTable.Length; r++)
            {
                TableRow DataRow = costTable.Rows[r + 1];
                DataRow.Height = 19;

                if(listMT[r].Status == true)
                    DataRow.RowFormat.BackColor = Color.LightGreen;
                else if (listMT[r].Status == false && listMT[r].LateFinish.CompareTo(DateTime.Now) < 0 )
                    DataRow.RowFormat.BackColor = Color.Red;
                else if (listMT[r].Status == false && listMT[r].LateFinish.CompareTo(DateTime.Now) > 0 )
                    DataRow.RowFormat.BackColor = Color.Yellow;

                for (int c = 0; c < dataTable[r].Length; c++) //c - column
                {
                    DataRow.Cells[c].CellFormat.VerticalAlignment = VerticalAlignment.Middle;




                    Paragraph par = DataRow.Cells[c].AddParagraph();
                    TextRange text = par.AppendText(dataTable[r][c]);
                    par.Format.HorizontalAlignment = HorizontalAlignment.Center;
                    text.CharacterFormat.FontName = "Calibri";
                    text.CharacterFormat.FontSize = 10;
                }
            }
        }



        private string[] getHeaderSubtask()
        {
            string[] headerSubtask = { "Name", "Desc.", "Pri.", "ST", "ET", "User", "Status" };

            return headerSubtask;
        }

        private string[][] getDataSubtask(Project project)
        {
            List<Subtask> tmpSubtask = new List<Subtask>();
            foreach (var tmp in project.MainTasks)
                foreach (var tmp2 in tmp.Subtasks)
                    tmpSubtask.Add(tmp2);

            int lenght = tmpSubtask.Count;
            string[][] data = new string[lenght][];
            int i = 0;

            foreach (var tmp in tmpSubtask)
            {
                string[] rowData = new string[7];
                rowData[0] = tmp.Name;
                rowData[1] = tmp.Description;
                rowData[2] = tmp.Priority.ToString();
                rowData[3] = tmp.StartTime.ToString("MM/dd/yy");
                rowData[4] = tmp.EndTime.ToString("MM/dd/yy");
                rowData[5] = tmp.User.FullName;
                rowData[6] = tmp.SubtaskStatus.Name;
                data[i] = rowData;
                i++;
            }

            return data;
        }


    }
}
