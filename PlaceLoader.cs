using System;
using System.Collections.Generic;
using CoreLocation;

namespace EmployeeApp.iOS.Helpers {
    public class PlaceLoader {
        public PlaceLoader() {
        }

        // static POIs for testing
        public Array GetStaticPOIsFor(CLLocation location) {

            Array returnPOIs = Array.CreateInstance(typeof(Dictionary<string, object>), 6);

            Dictionary<string, object> cube;

            cube = GeneratePOIDictFor(title: "Ramesh", location: new CLLocation(latitude: GetLatLongValue(33, 50, 55.749579), longitude: GetLatLongValue(84, 18, 17.151040)));
            returnPOIs.SetValue(cube, 0);

            cube = GeneratePOIDictFor(title: "Robert", location: new CLLocation(latitude: GetLatLongValue(33, 50, 55.720913), longitude: GetLatLongValue(84, 18, 17.199923)));
            returnPOIs.SetValue(cube, 1);

            cube = GeneratePOIDictFor(title: "Empty", location: new CLLocation(latitude: GetLatLongValue(33, 50, 55.872089), longitude: GetLatLongValue(84, 18, 17.367092)));
            returnPOIs.SetValue(cube, 2);

            cube = GeneratePOIDictFor(title: "Empty", location: new CLLocation(latitude: GetLatLongValue(33, 50, 55.825469), longitude: GetLatLongValue(84,18,17.449167)));
            returnPOIs.SetValue(cube, 3);

            cube = GeneratePOIDictFor(title: "Dolicia", location: new CLLocation(latitude: GetLatLongValue(33, 50, 55.755614), longitude: GetLatLongValue(84,18,17.418993)));
            returnPOIs.SetValue(cube, 4);

            cube = GeneratePOIDictFor(title: "Carol", location: new CLLocation(latitude: GetLatLongValue(33, 50, 55.833465), longitude: GetLatLongValue(84, 18, 17.415070)));
            returnPOIs.SetValue(cube, 5);


            return returnPOIs;
        }

        public static double GetLatLongValue(double degrees, double minutes, double seconds) {
            return degrees + ((minutes / 60) + (seconds / 3600));
        }

        // MARK: - Utility methods

        private Dictionary<string, object> GeneratePOIDictFor(string title, CLLocation location) {
                var poi = new Dictionary<string, object> {
                    { "name", title }
                };
                var loc = new Dictionary<string, double> {
                    { "lat", location.Coordinate.Latitude },
                    { "lng", location.Coordinate.Longitude }
                };

                var geom = new Dictionary<string, object> {
                    { "location", loc }
                };
                poi.Add("geometry", geom);

            return poi;
        }
    
        // extract CLLocation from dict
        public CLLocation GetLocationFrom(Dictionary<string, object> dict) {
            CLLocation location = null;

            var loc = dict["location"] as Dictionary<string, double>;
                
            if (loc != null) {
                var lat = loc["lat"] as double?;
                var lng = loc["lng"] as double?;
                location = new CLLocation(lat.Value, lng.Value);
            }
            return location;
        }

    /*
        private string SerializeParamsForRequest(params: [String: Any]) -> String {
            var paramArray = [String]()
            for (key, val) in params {
                let stringifiedVal = "\(val)"
                if key.isEmpty {
                    paramArray.append(stringifiedVal)
                }
                else if let escaped: String = stringifiedVal.addingPercentEncoding(withAllowedCharacters: .urlHostAllowed) {
                    let pair = "\(key)=\(escaped)"
                    paramArray.append(pair)
                }
            }
            return paramArray.joined(separator: "&")
        }
      */  
        // turn meters into relatable distance
        public (string, string) MetersToRecognizableString(double meters) {

            const double METERS_TO_FEET = 3.2808399;
            const double FEET_TO_MILES = 5280;
            const double FOOTBALL_FIELD = 300;
            
            var distance = meters * METERS_TO_FEET;

            
            if (distance<FOOTBALL_FIELD) {
                return (distance.ToString(), "feet");
            } else {
                var miles = distance / FEET_TO_MILES;
                return (miles.ToString(), "miles");
            }
        }
    }
}
