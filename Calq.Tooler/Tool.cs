using System;
using System.Reflection;

namespace Calq.Tooler {
    public class Tool {

        public static object? Execute(object instance, string[] args) {

            int ReadOptions(ParameterInfo[] parameters, string[] args, int startIndex, ref object[] paramValues) {
                var reader = new ParameterReader(parameters);
                foreach (var (option, value) in reader.Read(args, startIndex)) {
                    var (parameter, index) = GetParameterIndexPair(parameters, option);
                    var valueObj = Reflection.ParseValue(parameter.ParameterType, value, option);
                    paramValues[index] = valueObj;
                }

                return reader.LastIndex;
            }

            (ParameterInfo parameter, int index) GetParameterIndexPair(ParameterInfo[] parameters, string option) {
                for (var i = 0; i < parameters.Length; ++i) {
                    var param = parameters[i];
                    if (param.Name == option) {
                        return (param, i);
                    }
                }
                throw new Exception($"option doesn't exist: {option}");
            }

            var obj = instance;
            var i = 0;
            try {
                for (; i < args.Length; ++i) {
                    obj = Reflection.GetFieldOrPropertyValue(instance, args[i]);
                }
            } catch (MissingMemberException) {
                var method = obj.GetType().GetMethod(args[i]);
                if (method == null) {
                    throw new Exception($"invalid command");
                }

                ++i;
                var parameters = method.GetParameters();
                var parameterValues = new object[method.GetParameters().Length];
                if (i < args.Length) {
                    var parameterValuesIndex = 0;
                    var lastReadIndex = ReadOptions(parameters, args, i, ref parameterValues);
                    if (lastReadIndex == i) {
                        try {
                            parameterValues[parameterValuesIndex++] = args[i++];
                        } catch (IndexOutOfRangeException) {
                            throw new Exception($"incorrect usage: expected {method}");
                        }
                    } else {
                        i = lastReadIndex;
                    }
                    for (; i < args.Length; ++i) {
                        lastReadIndex = ReadOptions(parameters, args, i, ref parameterValues);
                        if (lastReadIndex != i) {
                            if (lastReadIndex < args.Length) {
                                throw new Exception($"incorrect usage: expected {method}");
                            }
                            break;
                        }
                        try {
                            parameterValues[parameterValuesIndex++] = args[i];
                        } catch (IndexOutOfRangeException) {
                            throw new Exception($"incorrect usage: expected {method}");
                        }
                    }

                    for (var j = 0; j < parameters.Length; ++j) {
                        if (parameters[j].ParameterType != parameterValues[j]?.GetType()) {
                            throw new Exception($"incorrect usage: expected {method}");
                        }
                    }
                }

                return method.Invoke(obj, parameterValues);
            }

            throw new Exception($"no command specified");
        }
    }
}
