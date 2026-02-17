
import {Route, Routes } from "react-router"
import { Navigate ,BrowserRouter } from  "react-router-dom"
import Teams from "./pages/Teams"
import Home from "./pages/Home"
import Races from "./pages/Races"
import User from "./pages/User"
import Drivers from "./pages/Drivers"
import Login from "./pages/Login"
import Prediction from "./pages/Prediction"
import Navbar from "./components/Navbar"
import Register from "./pages/Register"

function App() {
 
  const isAuthenticated = !!localStorage.getItem('accessToken');

  return (
    <BrowserRouter>
     { isAuthenticated && <Navbar />}
      <Routes>
      <Route path="/login"  element={isAuthenticated ? <Navigate to="/" /> : <Login />} />
      <Route path="/register"  element={<Register />} />
      
      <Route path="/" element= {isAuthenticated ? <Home/> : <Navigate to="/login" /> }/>
      <Route path="/Drivers" element= {isAuthenticated ? <Drivers/> : <Navigate to="/login" />}/>
      <Route path="/Teams" element= {isAuthenticated ? <Teams/> : <Navigate to="/login" />}/>
      <Route path="/Races" element= {isAuthenticated ? <Races/> : <Navigate to="/login" />}/>
      <Route path="/Prediction" element= {isAuthenticated ? <Prediction/> : <Navigate to="/login" />}/>
      <Route path="/User" element= {isAuthenticated ? <User/> : <Navigate to="/login" />}/>
      

     </Routes>
 
    </BrowserRouter>
  )
}

export default App
