import { BrowserRouter, Routes, Route } from "react-router-dom";
import "./App.css";
import Login from "./components/Login";
import Register from "./components/Register";
import Layout from "./components/Layout";
import ExamList from './components/ExamList';
import PrivateRoute from "./hooks/PrivateRoute";
import PatientList from "./components/PatientList";


function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Layout />}>
          <Route path="login" index element={<Login />} />
          <Route path="register" element={<Register />} />
          <Route
            path="exams"
            element={
              <PrivateRoute>
                <ExamList />
              </PrivateRoute>
            }
          />{" "}
          <Route
            path="patients"
            element={
              <PrivateRoute>
                <PatientList />
              </PrivateRoute>
            }
          />
        </Route>
      </Routes>
    </BrowserRouter>
  );
}

export default App;
