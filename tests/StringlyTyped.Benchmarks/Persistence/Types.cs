using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringlyTyped.Benchmarks.Persistence
{
    public class Vo_int_1 : ValueObject<int, Vo_int_1> { }
    public class Vo_int_2 : ValueObject<int, Vo_int_2> { }
    public class Vo_int_3 : ValueObject<int, Vo_int_3> { }
    public class Vo_int_4 : ValueObject<int, Vo_int_4> { }
    public class Vo_int_5 : ValueObject<int, Vo_int_5> { }
    public class Vo_int_6 : ValueObject<int, Vo_int_6> { }
    public class Vo_int_7 : ValueObject<int, Vo_int_7> { }
    public class Vo_int_8 : ValueObject<int, Vo_int_8> { }
    public class Vo_int_9 : ValueObject<int, Vo_int_9> { }
    public class Vo_int_10 : ValueObject<int, Vo_int_10> { }

    public class Vo_double_1 : ValueObject<double, Vo_double_1> { }
    public class Vo_double_2 : ValueObject<double, Vo_double_2> { }
    public class Vo_double_3 : ValueObject<double, Vo_double_3> { }
    public class Vo_double_4 : ValueObject<double, Vo_double_4> { }
    public class Vo_double_5 : ValueObject<double, Vo_double_5> { }
    public class Vo_double_6 : ValueObject<double, Vo_double_6> { }
    public class Vo_double_7 : ValueObject<double, Vo_double_7> { }
    public class Vo_double_8 : ValueObject<double, Vo_double_8> { }
    public class Vo_double_9 : ValueObject<double, Vo_double_9> { }
    public class Vo_double_10 : ValueObject<double, Vo_double_10> { }

    public class Vo_string_1 : ValueObject<string, Vo_string_1> { }
    public class Vo_string_2 : ValueObject<string, Vo_string_2> { }
    public class Vo_string_3 : ValueObject<string, Vo_string_3> { }
    public class Vo_string_4 : ValueObject<string, Vo_string_4> { }
    public class Vo_string_5 : ValueObject<string, Vo_string_5> { }
    public class Vo_string_6 : ValueObject<string, Vo_string_6> { }
    public class Vo_string_7 : ValueObject<string, Vo_string_7> { }
    public class Vo_string_8 : ValueObject<string, Vo_string_8> { }
    public class Vo_string_9 : ValueObject<string, Vo_string_9> { }
    public class Vo_string_10 : ValueObject<string, Vo_string_10> { }

    public class Container
    {
        public List<Vo_int_1> Vo1_ints = new();
        public List<Vo_int_2> Vo2_ints = new();
        public List<Vo_int_3> Vo3_ints = new();
        public List<Vo_int_4> Vo4_ints = new();
        public List<Vo_int_5> Vo5_ints = new();
        public List<Vo_int_6> Vo6_ints = new();
        public List<Vo_int_7> Vo7_ints = new();
        public List<Vo_int_8> Vo8_ints = new();
        public List<Vo_int_9> Vo9_ints = new();
        public List<Vo_int_10> Vo10_ints = new();

        public List<Vo_double_1> Vo1_doubles = new();
        public List<Vo_double_2> Vo2_doubles = new();
        public List<Vo_double_3> Vo3_doubles = new();
        public List<Vo_double_4> Vo4_doubles = new();
        public List<Vo_double_5> Vo5_doubles = new();
        public List<Vo_double_6> Vo6_doubles = new();
        public List<Vo_double_7> Vo7_doubles = new();
        public List<Vo_double_8> Vo8_doubles = new();
        public List<Vo_double_9> Vo9_doubles = new();
        public List<Vo_double_10> Vo10_doubles = new();

        public List<Vo_string_1> Vo1_strings = new();
        public List<Vo_string_2> Vo2_strings = new();
        public List<Vo_string_3> Vo3_strings = new();
        public List<Vo_string_4> Vo4_strings = new();
        public List<Vo_string_5> Vo5_strings = new();
        public List<Vo_string_6> Vo6_strings = new();
        public List<Vo_string_7> Vo7_strings = new();
        public List<Vo_string_8> Vo8_strings = new();
        public List<Vo_string_9> Vo9_strings = new();
        public List<Vo_string_10> Vo10_strings = new();
    }
}
