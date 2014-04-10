using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Alsolos.AttendanceRecorder.TestClient {
    public class UrlBuilder {
        private const string _baseUrl = "http://localhost:12345/api";

        public Uri Get(Controller controller, params string[] parameters) {
            var controllerUrl = GetControllerUrl(controller);
            var parametersUrl = GetParametersUrl(parameters);
            return new Uri(string.Format("{0}/{1}/{2}", _baseUrl, controllerUrl, parametersUrl));
        }

        public static string GetParametersUrl(params string[] parameters) {
            if (parameters == null || parameters.Length == 0) {
                return string.Empty;
            }
            if (parameters.Length % 2 > 0) {
                throw new InvalidOperationException("Parameters count must be an even number.");
            }
            var parts = new List<string>();
            for (var i = 0; i < parameters.Length; i += 2) {
                parts.Add(string.Format("{0}={1}", parameters[i], parameters[i + 1]));
            }
            var allParts = string.Join("&", parts);
            return "?" + allParts;
        }

        public static string GetControllerUrl(Enum value) {
            var attributes = (DescriptionAttribute[])value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }

        public enum Controller {
            [Description("interval")]
            Interval,
            [Description("lifeSign")]
            LifeSign,
        }
    }
}
