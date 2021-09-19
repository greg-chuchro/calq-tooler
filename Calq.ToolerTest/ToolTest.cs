using Calq.Tooler;
using System;
using Xunit;

namespace Calq.ToolerTest {
    public class ToolTest {
        [Fact]
        public void Test1() {
            var tool = new TestTool();
            var result = Tool.Execute(tool, new[] { "Text", "--text", "abc" });
            Assert.Equal("abc", result);
        }

        [Fact]
        public void Test2() {
            var tool = new TestTool();
            var result = Tool.Execute(tool, new[] { "Text", "abc" });
            Assert.Equal("abc", result);
        }

        [Fact]
        public void Test3() {
            var tool = new TestTool();
            var result = Tool.Execute(tool, new[] { "Integer", "--integer", "1" });
            Assert.Equal(1, result);
        }

        [Fact]
        public void Test4() {
            var ex = Assert.Throws<Exception>(() => {
                var tool = new TestTool();
                Tool.Execute(tool, new[] { "Integer", "1" });
            });
            Assert.Equal("incorrect usage: expected Int32 Integer(Int32)", ex.Message);
        }

        [Fact]
        public void Test5() {
            var tool = new TestTool();
            var result = Tool.Execute(tool, new[] { "TextAndInteger", "abc", "--integer", "1" });
            Assert.Null(result);
            Assert.Equal("abc", tool.text);
            Assert.Equal(1, tool.integer);
        }

        [Fact]
        public void Test6() {
            var tool = new TestTool();
            var result = Tool.Execute(tool, new[] { "IntegerAndText", "--integer", "1", "--text", "abc" });
            Assert.Null(result);
            Assert.Equal(1, tool.integer);
            Assert.Equal("abc", tool.text);
        }

        [Fact]
        public void Test7() {
            var ex = Assert.Throws<Exception>(() => {
                var tool = new TestTool();
                var result = Tool.Execute(tool, new[] { "IntegerAndText", "--integer", "1", "abc" });
            });
            Assert.Equal("incorrect usage: expected Void IntegerAndText(Int32, System.String)", ex.Message);
        }

        [Fact]
        public void Test8() {
            var ex = Assert.Throws<Exception>(() => {
                var tool = new TestTool();
                var result = Tool.Execute(tool, new[] { "IntegerAndText", "abc", "--integer", "1" });
            });
            Assert.Equal("incorrect usage: expected Void IntegerAndText(Int32, System.String)", ex.Message);
        }

        [Fact]
        public void Test9() {
            var tool = new TestTool();
            var result = Tool.Execute(tool, new[] { "TextAndInteger", "--integer", "1", "abc" });
            Assert.Null(result);
            Assert.Equal("abc", tool.text);
            Assert.Equal(1, tool.integer);
        }

        [Fact]
        public void Test11() {
            var tool = new TestTool();
            Tool.Execute(tool, new[] { "Foo" });
        }

        [Fact]
        public void Test12() {
            var ex = Assert.Throws<Exception>(() => {
                var tool = new TestTool();
                Tool.Execute(tool, new[] { "Foo", "abc" });
            });
            Assert.Equal("incorrect usage: expected Void Foo()", ex.Message);
        }

        [Fact]
        public void Test13() {
            var ex = Assert.Throws<Exception>(() => {
                var tool = new TestTool();
                Tool.Execute(tool, new[] { "Undefined" });
            });
            Assert.Equal("invalid command", ex.Message);
        }

        [Fact]
        public void Test14() {
            var tool = new TestTool();
            Tool.Execute(tool, new[] { "inner", "InnerFoo" });
        }

        [Fact]
        public void Test15() {
            var ex = Assert.Throws<NullReferenceException>(() => {
                var tool = new TestTool();
                Tool.Execute(tool, new[] { "nullInner", "InnerFoo" });
            });
        }
    }
}
