import {
  Button,
  Card,
  CardContent,
  FormControl,
  Grid2,
  IconButton,
  Input,
  InputAdornment,
  Stack,
  Typography,
} from "@mui/material";
import React, { useEffect, useState } from "react";
import { createAPIEndpoint, EndPoints } from "../API/index";
import { Link, useNavigate } from "react-router-dom";
import { Visibility, VisibilityOff } from "@mui/icons-material";
import { enqueueSnackbar } from "notistack";

const InitialValues = {
  email: "",
  password: "",
};
function Login() {
  const [values, setValues] = useState(InitialValues);
  const [errors, setErrors] = useState({});
  const [isSubmit, setIsSubmit] = useState(false);
  const [showPassword, setShowPassword] = useState(false);

  const handleClickShowPassword = () => {
    setShowPassword((show) => !show);
  };
  const handleMouseDownPassword = (e) => {
    e.preventDefault();
  };
  const handleMouseUpPassword = (e) => {
    e.preventDefault();
  };

  const handleInputChanged = (e) => {
    const { name, value } = e.target;
    setValues({
      ...values,
      [name]: value,
    });
  };
  const navigate = useNavigate();
  const validation = (formValues) => {
    const errors = {};
    const regex = /^[^\s@]+@[^\s@]+\.[^\s@]{2,}$/i;
    if (!formValues.email) errors.email = "Please enter your email address.";
    else if (!regex.test(formValues.email)) errors.email = "Email is Invalid.";
    if (!formValues.password) errors.password = "Please enter your password.";
    return errors;
  };
  const handleSubmitForm = (e) => {
    e.preventDefault();
    setErrors(validation(values));
    setIsSubmit(true);
  };
  const resetForm = () => {
    setValues(InitialValues);
    setErrors({});
    setIsSubmit(false);
  };
  useEffect(() => {
    if (Object.keys(errors).length === 0 && isSubmit) {
      createAPIEndpoint(EndPoints.Account.login)
        .post(values)
        .then((res) => {
          localStorage.setItem("token", res.data.token);
          navigate("/exams");
          enqueueSnackbar("Login Success.", { variant: "success" });
        })
        .catch((err) =>{ enqueueSnackbar("Login Failed.", { variant: "error" }); console.log(err)});
      resetForm();
    }
  }, [errors]);

  return (
    <div className="login">
      <Grid2
        container
        justifyContent="center"
        alignContent="center"
        minHeight="100vh"
      >
        <Card sx={{ width: 360, borderRadius: 5 }} elevation={10}>
          <CardContent>
            <Typography
              variant="h4"
              color="primary"
              fontWeight="bold"
              textAlign="center"
              marginBottom="20px"
            >
              Login
            </Typography>
            <form autoComplete="off" onSubmit={handleSubmitForm} noValidate>
              <Stack sx={{ gap: 2 }}>
                <FormControl variant="standard">
                  <Input
                    placeholder="Email"
                    type="email"
                    name="email"
                    value={values.email}
                    onChange={handleInputChanged}
                    required
                  />
                  <p className="err-hint">{errors.email}</p>
                </FormControl>

                <FormControl variant="standard">
                  <Input
                    placeholder="Password"
                    type={showPassword ? "text" : "password"}
                    name="password"
                    value={values.password}
                    onChange={handleInputChanged}
                    required
                    endAdornment={
                      <InputAdornment position="end">
                        <IconButton
                          aria-label="toggle password visibility"
                          onClick={handleClickShowPassword}
                          onMouseDown={handleMouseDownPassword}
                          onMouseUp={handleMouseUpPassword}
                        >
                          {showPassword ? <VisibilityOff /> : <Visibility />}
                        </IconButton>
                      </InputAdornment>
                    }
                  />
                  <p className="err-hint">{errors.password}</p>
                </FormControl>
                <Button variant="contained" type="submit">
                  Submit
                </Button>
                <span className="nav-hint">
                  Don't have an account?{" "}
                  <Typography component={Link} to="/register" color="primary">
                    Register
                  </Typography>
                </span>
              </Stack>
            </form>
          </CardContent>
        </Card>
      </Grid2>
    </div>
  );
}

export default Login;
