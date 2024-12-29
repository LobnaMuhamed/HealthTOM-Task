import React, { createContext, useState, useContext, useEffect } from "react";

const ThemeContext = createContext();

export const ThemeProvider = ({ children }) => {
  const [isLight, setIsLight] = useState(()=> {
    const storedTheme = localStorage.getItem("theme");
    return storedTheme? storedTheme === "light" : true;
  });

  const toggleTheme = () => {
    setIsLight((prev) => !prev) 
  }
  useEffect(()=>
    {
    if (isLight) {
    localStorage.setItem("theme", "light");
    localStorage.removeItem("dark");
  } else {
    localStorage.setItem("theme", "dark");
    localStorage.removeItem("light");
  }}, [isLight])
    ;

  return (
    <ThemeContext.Provider value={{ isLight, toggleTheme }}>
      {children}
    </ThemeContext.Provider>
  );
};

export const useTheme = () => useContext(ThemeContext);