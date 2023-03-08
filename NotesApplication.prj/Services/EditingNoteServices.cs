using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NotesApplication.Services
{
	public class EditingNoteServices
	{
		private static List<FontFamily> fontNames = Fonts.SystemFontFamilies.OrderBy(f => f.Source).ToList(); //список шрифтов
		private static List<double> fontSize = new List<double> { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 }; //список размера шрифта


		public static List<FontFamily> FontNames
		{
			get { return fontNames; }
		}

		public static List<double> FontSize
		{
			get { return fontSize; }
		}

		/// <summary>
		/// Добавление заметки к изображению
		/// </summary>
		/// <param name="imageFileName">название файла с изображением</param>
		/// <param name="nameForAddingImage">название используемого RichTextBox для вывода заметок</param>
		public void AddImageToNote(string imageFileName, RichTextBox nameForAddingImage)
		{
			BitmapImage bitmapImageForConvert = new BitmapImage();
			bitmapImageForConvert.BeginInit();
			bitmapImageForConvert.UriSource = new Uri(imageFileName, UriKind.Relative);
			bitmapImageForConvert.EndInit();

			Image imageForNote = new Image();
			imageForNote.Height = 100;
			imageForNote.Width = 100;
			imageForNote.Source = bitmapImageForConvert;

			InlineUIContainer container = new InlineUIContainer(imageForNote);
			nameForAddingImage.Document.Blocks.Add(new Paragraph(container));
		}

		/// <summary>
		/// Сделать выделенный текст полужирным
		/// </summary>
		/// <param name="nameOfRichTextBoxForApplyPropetry">название используемого RichTextBox для вывода заметок</param>
		public void makeSelectTextBold(RichTextBox nameOfRichTextBoxForApplyPropetry) 
		{
			if(nameOfRichTextBoxForApplyPropetry.Selection.GetPropertyValue(Inline.FontWeightProperty) != DependencyProperty.UnsetValue)
			{
				FontWeight currentWeight = (FontWeight)nameOfRichTextBoxForApplyPropetry.Selection.GetPropertyValue(Inline.FontWeightProperty);
				if(currentWeight == FontWeights.Bold)
				{
					nameOfRichTextBoxForApplyPropetry.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Normal);
				}
				else
				{
					nameOfRichTextBoxForApplyPropetry.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Bold);
				}
			}
		}

		/// <summary>
		/// Сделать выделенный текст курсивом
		/// </summary>
		/// <param name="nameOfRichTextBoxForApplyPropetry">название используемого RichTextBox для вывода заметок</param>
		public void makeSelectTextItalic(RichTextBox nameOfRichTextBoxForApplyPropetry) 
		{
			if(nameOfRichTextBoxForApplyPropetry.Selection.GetPropertyValue(Inline.FontStyleProperty) != DependencyProperty.UnsetValue)
			{
				FontStyle currentStyle = (FontStyle)nameOfRichTextBoxForApplyPropetry.Selection.GetPropertyValue(Inline.FontStyleProperty);
				if(currentStyle == FontStyles.Italic)
				{
					nameOfRichTextBoxForApplyPropetry.Selection.ApplyPropertyValue(Inline.FontStyleProperty, FontStyles.Normal);
				}
				else
				{
					nameOfRichTextBoxForApplyPropetry.Selection.ApplyPropertyValue(Inline.FontStyleProperty, FontStyles.Italic);
				}
			}
		}

		/// <summary>
		/// Сделать выделенный текст подчеркнутым
		/// </summary>
		/// <param name="nameOfRichTextBoxForApplyPropetry">название используемого RichTextBox для вывода заметок</param>
		public void makeSelectTextUnderlined(RichTextBox nameOfRichTextBoxForApplyPropetry)
		{
			if(nameOfRichTextBoxForApplyPropetry.Selection.GetPropertyValue(Inline.TextDecorationsProperty) != DependencyProperty.UnsetValue)
			{
				TextDecorationCollection currentDecorations = (TextDecorationCollection)nameOfRichTextBoxForApplyPropetry.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
				if(currentDecorations == null || currentDecorations.Count == 0)
				{
					nameOfRichTextBoxForApplyPropetry.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
				}
				else
				{
					nameOfRichTextBoxForApplyPropetry.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, null);
				}
			}
		}

		/// <summary>
		/// Изменить размер шрифта
		/// </summary>
		/// <param name="nameOfRichTextBoxForApplyPropetry">название используемого RichTextBox для вывода заметок</param>
		/// <param name="comboBoxWithValues">comboBox с значениями размера шрифта</param>
		public void changeFontSize(RichTextBox nameOfRichTextBoxForApplyPropetry, ComboBox comboBoxWithValues)
		{
			if(nameOfRichTextBoxForApplyPropetry.Selection.IsEmpty)
				nameOfRichTextBoxForApplyPropetry.FontSize = Convert.ToDouble(comboBoxWithValues.SelectedItem);
			else
				nameOfRichTextBoxForApplyPropetry.Selection.ApplyPropertyValue(TextElement.FontSizeProperty, comboBoxWithValues.SelectedItem);
		}

		/// <summary>
		/// Изменить шрифт
		/// </summary>
		/// <param name="nameOfRichTextBoxForApplyPropetry">название используемого RichTextBox для вывода заметок</param>
		/// <param name="comboBoxWithValues">comboBox со списком шрифтов</param>
		public void changeFontFamily(RichTextBox nameOfRichTextBoxForApplyPropetry, ComboBox comboBoxWithValues)
		{
			if(comboBoxWithValues.SelectedItem != null)
				nameOfRichTextBoxForApplyPropetry.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, comboBoxWithValues.SelectedItem);
		}
	}
}
