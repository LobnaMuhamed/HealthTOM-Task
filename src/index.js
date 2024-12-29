import React from "react";
import ReactDOM from "react-dom/client";
import "./index.css";
import App from "./App";
import reportWebVitals from "./reportWebVitals";
import {
  CssBaseline,
  ThemeProvider as MuiThemeProvider,
  createTheme,
} from "@mui/material";
import { ThemeProvider, useTheme } from "../src/context/ThemeContext";
import { SnackbarProvider } from "notistack";

const AppWithTheme = () => {
  const { isLight } = useTheme();

  const theme = createTheme({
    palette: {
      mode: isLight ? "light" : "dark",
      primary: {
        main: "#4daffa",
      },
      typography: {
        fontFamily: "IBM Plex Sans",
      },
    },
  });

  return (
    <MuiThemeProvider theme={theme}>
      <CssBaseline />
      <App />
    </MuiThemeProvider>
  );
};

const root = ReactDOM.createRoot(document.getElementById("root"));
root.render(
  <React.StrictMode>
    <SnackbarProvider maxSnack={3}>
      <ThemeProvider>
        <AppWithTheme />
      </ThemeProvider>
    </SnackbarProvider>
  </React.StrictMode>
);
reportWebVitals();
