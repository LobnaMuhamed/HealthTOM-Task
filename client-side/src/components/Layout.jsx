import React, { useEffect, useState } from "react";
import { AppBar, Toolbar, IconButton, Container, Stack, Typography } from "@mui/material";
import { LightMode, DarkMode, Logout } from "@mui/icons-material";
import { useTheme } from "../context/ThemeContext";
import { Link, Outlet, useNavigate } from "react-router-dom";
import Image from "../images/HealthTOM-Logo.png";
export default function Layout() {
  const { isLight, toggleTheme } = useTheme();
  const [isLogin, setIsLogin] = useState(() => !!localStorage.getItem("token"));

  const navigate = useNavigate();

  const handleLogout = () => {
    if (isLogin) {
      localStorage.removeItem("token");
      setIsLogin(false);
      navigate("/login");
    }
  };

  useEffect(() => {
    var token = localStorage.getItem("token");
    if (!token) navigate("/login");
    else {
      setIsLogin(true)
    }
  }, [isLogin, navigate]);

  return (
    <>
      <AppBar position="sticky">
        <Toolbar sx={{ justifyContent: "space-between" }}>
          <Stack direction="row" sx={{gap:3}}> 
            <img src={Image} alt="img" className="logo-img" />
            <Typography component={Link} to="exams" color="white" variant="h6">
              Exams
            </Typography>
            <Typography component={Link} to="patients" color="white" variant="h6">
              Patients
            </Typography>
          </Stack>
          <Stack direction="row">
            {" "}
            <IconButton onClick={toggleTheme}>
              {isLight ? (
                <LightMode fontSize="large" />
              ) : (
                <DarkMode fontSize="large" />
              )}
            </IconButton>
            {isLogin && (
              <IconButton onClick={handleLogout}>
                <Logout fontSize="large" />
              </IconButton>
            )}
          </Stack>
        </Toolbar>
      </AppBar>
      <Container>
        <Outlet />
      </Container>
    </>
  );
}
