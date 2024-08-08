import React, { useState } from 'react';
import axios from 'axios';
import { Link, useNavigate } from 'react-router-dom';
import './Register.css';

axios.defaults.baseURL = 'http://localhost:7066'; // Promenite https u http

const Register = () => {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [error, setError] = useState('');
  const navigate = useNavigate();

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    if (name === 'username') {
      setUsername(value);
    } else if (name === 'password') {
      setPassword(value);
    } else if (name === 'confirmPassword') {
      setConfirmPassword(value);
    }
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (password !== confirmPassword) {
      setError('Passwords do not match');
      return;
    }

    try {
      const response = await axios.post('/api/User/register', { username, password });
      if (response.status === 201) {
        navigate('/');
      }
    } catch (error) {
      const errorMsg = error.response?.data?.errors ? Object.values(error.response.data.errors).flat().join(', ') : error.response?.data || 'There was an error registering';
      setError(errorMsg);
    }
  };

  return (
    <div className="sve">
      <form onSubmit={handleSubmit}>
        <h1>Register</h1>
        {error && <p className="error">{error}</p>}
        <div className="input">
          <input type="text" name="username" placeholder="Username" required onChange={handleInputChange} />
          <i className="bx bxs-user"></i>
        </div>
        <div className="input">
          <input type="password" name="password" placeholder="Password" required onChange={handleInputChange} />
          <i className="bx bxs-lock-alt"></i>
        </div>
        <div className="input">
          <input type="password" name="confirmPassword" placeholder="Confirm Password" required onChange={handleInputChange} />
          <i className="bx bxs-lock-alt"></i>
        </div>
        <button type="submit" className="dugme">Register</button>
        <div className="login">
          <p>Already have an account? <Link to="/">Login</Link></p>
        </div>
      </form>
    </div>
  );
};

export default Register;
