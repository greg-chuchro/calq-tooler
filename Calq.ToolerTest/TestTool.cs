﻿#pragma warning disable CS0649

namespace Ghbvft6.Calq.ToolerTest {
    class TestTool {

        public class Inner {
            public void InnerFoo() { }
        }

        public string text;
        public int integer;

        public Inner inner = new Inner();
        public Inner nullInner;

        public void Foo() { }

        public string Text(string text) {
            return text;
        }

        public int Integer(int integer) {
            return integer;
        }

        public void TextAndInteger(string text, int integer) {
            this.text = text;
            this.integer = integer;
        }

        public void IntegerAndText(int integer, string text) {
            this.integer = integer;
            this.text = text;
        }
    }
}
