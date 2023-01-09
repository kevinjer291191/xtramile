import './App.css';
import React, { useState, useEffect } from 'react';
import axios from 'axios';
import config from './config';
import Select from 'react-select';


const CountryComboBox = ({ onChange }) => {
  const [options, setOptions] = useState([]);

  useEffect(() => {
    // Fetch the list of countries from the local API
    axios.get(`${config.baseURI}/api/location/countries`)
      .then(response => {
        // Map the API response to the options format required by react-select
        const options = response.data.map(country => ({
          value: country.CountryCode,
          label: country.CountryName
        }));
        setOptions(options);
      });
  }, []);

  return (
    <Select options={options} onChange={onChange} />
  );
};

const CityComboBox = ({ countryCode, onChange }) => {
  const [options, setOptions] = useState([]);

  useEffect(() => {
    if (countryCode) {
      // Fetch the list of cities from the local API
      axios.get(`${config.baseURI}/api/location/cities/${countryCode}`)
        .then(response => {
          // Map the API response to the options format required by react-select
          const options = response.data.map(city => ({
            value: city.CityName,
            label: city.CityName
          }));
          setOptions(options);
        });
    } else {
      setOptions([]);
    }
  }, [countryCode]);

  return (
    <Select options={options} onChange={onChange} />
  );
};

const App = () => {
  const [selectedCountry, setSelectedCountry] = useState(null);
  const [selectedCity, setSelectedCity] = useState(null);
  const [weather, setWeather] = useState(null);
  const [error, setError] = useState(null);

  const handleCountryChange = selectedOption => {
    setSelectedCountry(selectedOption);
    setSelectedCity(null);
    setWeather(null);
  };

  const handleCityChange = selectedOption => {
    setSelectedCity(selectedOption);
    // Query the local weather API to get the weather for the selected city appended with country
    axios.get(`${config.baseURI}/api/weather/city/${selectedOption.value},${selectedCountry.value}`)
    .then(response => {
      console.log(response);
      if (response.status == 200) {

        setWeather(response.data);
        setError(null);
      } else {
        console.log("hit");
        setWeather(null);
        setError('Weather is not found in this location, please try another location');
      }
    }).catch(error => {
      setWeather(null);
      setError('Weather is not found in this location, please try another location.');
    });
};

      
  return (
    <div>
      <h1>Weather App Frontend</h1>
      <div>
        <label>Country:</label>
        <CountryComboBox onChange={handleCountryChange} />
      </div>
      <div>
        <label>City:</label>
        <CityComboBox countryCode={selectedCountry ? selectedCountry.value : null} onChange={handleCityChange} />
        </div>
      {error && (
        <div>
          <p>{error}</p>
        </div>
      )}
      {weather && (
        <div>
          <h2>Weather Details</h2>
          <h2>{weather.Location} - {weather.Time}</h2>
          <div>
            <img src={weather.Icon} alt={weather.Description} />
            <p>{weather.Description} - {weather.SkyCondition}</p>
          </div>
          <hr/>
          <h3>Celcius</h3>
          <p>Current temperature: {weather.TemperatureC.toFixed(2)}°C</p>
          <p>Min temperature: {weather.TemperatureCMin.toFixed(2)}°C</p>
          <p>Max temperature: {weather.TemperatureCMax.toFixed(2)}°C</p>
          <hr/>
          <h3>Fahrenheit</h3>
          <p>Current temperature: {weather.TemperatureF.toFixed(2)}°F</p>
          <p>Min temperature: {weather.TemperatureFMin.toFixed(2)}°F</p>
          <p>Max temperature: {weather.TemperatureFMax.toFixed(2)}°F</p>
          <hr/>
          <h3>Wind Detail</h3>
          <p>Degree: {weather.WindDetail.Degree.toFixed(2)}</p>
          <p>Speed: {weather.WindDetail.Speed.toFixed(2)}</p>
          <hr/>
          <h3>Other Detail</h3>
          <p>Humidity: {weather.Humidity.toFixed(0)}</p>
          <p>Pressure: {weather.Pressure.toFixed(0)}</p>
          <p>Visibility: {weather.Visibility.toFixed(0)}</p>
        </div>
      )}
    </div>
  );
};

export default App;

