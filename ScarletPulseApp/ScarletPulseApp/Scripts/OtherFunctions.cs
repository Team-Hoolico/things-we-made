using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScarletPulseApp.OtherFunctions {
    static class DateTimeFunctions {
        public static string GetGreeting() {
            return DateTime.Now.Hour switch {
                (>= 6) and (<= 12) => "Morning",
                (> 12) and (<= 18) => "Afternoon",
                (> 18) => "Evening",
                (< 6) => "Night",
            };
        }

    }
}
