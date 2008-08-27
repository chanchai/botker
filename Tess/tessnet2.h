///////////////////////////////////////////////////////////////////////
// File:        tessnet2.h
// Description: .NET Assembly for Tesseract.
// Author:      Remi THOMAS
// Created:     21JUN08
// Version:		2.03.3
//
// (C) Copyright 2008, Pixel Technology.
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
///////////////////////////////////////////////////////////////////////
// tessnet2.h : The assembly definition.
//

#pragma once

using namespace System;
using namespace System::Drawing;
using namespace System::Threading;
using namespace System::Collections::Generic;

#include "ocrblock.h"
#include "ocrclass.h"
#include "ocrshell.h"
#include "pageres.h"

class MyTessBaseAPI;

namespace tessnet2 
{

	public ref class Character
	{
	public:
		// Value
		Char^ Value;
		// Character location
		int Left, Right, Top, Bottom;
		// Constructro
		Character(Char^ character, int left, int top, int right, int bottom)
		{
			Value = character;
			Left = left; Top = top; Right = right; Bottom = bottom;
		}
		// All other values are the same for the Word and all is characters (Confidence, FontIndex, PointSize, Formating...)
	};

	public ref class Word
	{
	public:
		// The line index for this word
		int LineIndex;
		// Blanks count
		int Blanks;
		// Word location
		int Left, Right, Top, Bottom;
		// Some value directly copied from tessract
		int FontIndex, PointSize, Formating;
		// The text
		String^ Text;
		// confidence : 0 = perfect; 255 = reject, over 160 = bad reco
		// This value is sensitive to Tesseract variable tessedit_write_ratings and tessedit_zero_rejection
		// After many test I found tessedit_write_ratings=true give the best result, this is the default mode
		// in tessnet2, you can change this setting the variables before calling DoOCR
		Double Confidence;
		// Character position
		List<Character ^>^ CharList;

		virtual String^ ToString() override
		{
			return String::Format("{0} ({1})", Text, Confidence);
		}
	};

	public ref class Tesseract
	{
		// TessBaseAPI instance
		MyTessBaseAPI *	m_myTessBaseAPIInstance;

		ETEXT_DESC* m_monitor;
		PAGE_RES *m_pageRes;

		Thread^ m_ocrThread;
		void doOCR();
		Thread^ m_callbackThread;
		void doCallback();
		List<tessnet2::Word^>^ BuildPage();
	public:
		Tesseract();
		~Tesseract();

		// Get completion percent info in OcrPage::PercentDone
		delegate void ProgressHandler(int percent);
		event ProgressHandler^ ProgressEvent;
		// Callback delegate when OCR is done
		delegate void OcrDoneHandler(List<Word ^>^ words);
		OcrDoneHandler^ OcrDone;

		// Tesseract API can't force DLL path for tessdata
		// lang = ENG, FRA...
		// Call Init before calling any other method
		int								Init			(String^ lang, bool numericMode);
		void							Clear			();

		// Tesseract variables
		// Assign tessedit_char_whitelist or tessedit_char_blacklist before calling Init
		// SetVariable("tessedit_char_whitelist", "0123456789") 
		bool							SetVariable		(String^ name, Object^ val);
		Object^							GetVariable		(String^ name);
		Dictionary<String ^, Type^>^	GetVariableList	();

		// OCR
		// bitmap, 1, 8 ou 24 bpp images
		// Rectangle::Empty for full page
		// if OcrDoneEvnet is null the OCR is synchronous, else asynchronous and callback when thread finished
		List<Word ^>^	DoOCR			(System::Drawing::Bitmap^ bitmap, System::Drawing::Rectangle rect);

		// Return number of line
		static	int				LineCount	(List<Word ^>^ words);
		// Return word for the designated line
		static	List<Word ^>^ 	GetLineWords(List<Word ^>^ words, int lineIndex);
		// Build a line
		static	String^ 		GetLineText	(List<Word ^>^ words, int lineIndex);
	};
}
