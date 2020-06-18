﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;


namespace Safely.Model
{
    public class User
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public StatusEnum Status { get; set; }
        public enum StatusEnum
        {
            Healthy,
            Symptomatic,
            Diagnosed,
            Recovered
        }

        public User() { }
        public User(string Email, string Password)
        {
            this.Email = Email;
            this.Password = Password;
        }

        public void UpdateLocation(float longitude, float latitude)
        {
            this.Longitude = longitude;
            this.Latitude = latitude;
        }

        public void UpdateStatus(StatusEnum status)
        {
            this.Status = status;
        }

        public async Task UpdateLocation()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                var location = await Geolocation.GetLocationAsync(request);

                if (location != null)
                {
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    this.Longitude = location.Longitude;
                    this.Latitude = location.Latitude;

                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                Debug.WriteLine("Failed: FeatureNotSupportedException");
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                Debug.WriteLine("Failed: FeatureNotEnabledException");
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                Debug.WriteLine("Failed: PermissionException");
                // Handle permission exception
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Failed: Unknown Exception");
                // Unable to get location
            }
        }
    }
}
