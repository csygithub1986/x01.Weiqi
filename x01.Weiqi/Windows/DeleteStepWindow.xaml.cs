﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using x01.Weiqi.Models;

namespace x01.Weiqi.Windows
{
	/// <summary>
	/// DeleteStepWindow.xaml 的交互逻辑
	/// </summary>
	public partial class DeleteStepWindow : Window
	{
		public DeleteStepWindow()
		{
			InitializeComponent();
			m_Id.ItemsSource = GetIds();
			m_Id.SelectedIndex = 0;
		}

		List<int> GetIds()
		{
			using (var db = new WeiqiContext()) {
				var result = from r in db.Records
							 select r.Id;
				return result.ToList();
			}
		}

		private void Id_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			int id = (int)m_Id.SelectedValue;
			using (var db = new WeiqiContext()) {
				var result = (from r in db.Records
							  where r.Id == id
							  select r).First();
				m_BlackName.Text = result.Black;
				m_WhiteName.Text = result.White;
				m_Result.Text = result.Result;
				m_Description.Text = result.Description;
				m_Type.Text = result.Type;
			}
		}

		private void Delete_Click(object sender, RoutedEventArgs e)
		{
			if (m_Id.SelectedValue == null) {
				MessageBox.Show("Please select Id.");
				return;
			}
			int id = (int)m_Id.SelectedValue;
			using (var db = new WeiqiContext()) {
				// delete all
//				var records = db.Records.ToList();
//				foreach (var r in records) {
//					db.Records.Remove(r);
//				}
				var record = db.Records.Where(r => r.Id == id).First();
				db.Records.Remove(record);
				db.SaveChanges();
				MessageBox.Show("Delete success!");
				Close();
			}
		}

		private void Cancel_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}
	}
}
