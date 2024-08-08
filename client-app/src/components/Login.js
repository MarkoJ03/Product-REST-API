import React, { useState } from 'react';
import axios from 'axios';
import { Link, useNavigate } from 'react-router-dom';
import './Login.css';

axios.defaults.baseURL = 'http://localhost:7066'; // Promenite https u http

const Login = () => {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const navigate = useNavigate();

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    if (name === 'username') {
      setUsername(value);
    } else {
      setPassword(value);
    }
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      const response = await axios.post('/api/Auth/login', { username, password });
      console.log(response.data); // Proverite odgovor
      localStorage.setItem('token', response.data.token);
      
      navigate('/Products'); // Primer preusmeravanja
    } catch (error) {
      console.error('There was an error logging in!', error);
    }
  };

  return (
    <div className="sve">
      <form onSubmit={handleSubmit}>
        <h1>Login</h1>
        <div className="input">
          <input type="text" name="username" placeholder="Username" required onChange={handleInputChange} />
          <i className="bx bxs-user"></i>
        </div>
        <div className="input">
          <input type="password" name="password" placeholder="Password" required onChange={handleInputChange} />
          <i className="bx bxs-lock-alt"></i>
        </div>
        <button type="submit" className="dugme">Login</button>
        <div className="registracija">
          <p>Don't have an account? <Link to="/register">Register</Link></p>
        </div>
      </form>
    </div>
  );
};

export default Login;
