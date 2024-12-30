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
  firstName: "",
  lastName: "",
  email: "",
  password: "",
  confirmPassword: "",
};
function Register() {
  const [values, setValues] = useState(InitialValues);
  const [errors, setErrors] = useState({});
  const [isSubmit, setIsSubmit] = useState(false);
  const [showPassword, setShowPassword] = useState(false);
  const [showConfirmPassword, setShowConfirmPassword] = useState(false);

  const handleClickShowPassword = () => {
    setShowPassword((show) => !show);
  };
  const handleClickShowConfirmPassword = () => {
    setShowConfirmPassword((show) => !show);
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
    const regexEmail = /^[^\s@]+@[^\s@]+\.[^\s@]{2,}$/i;
    if (!formValues.firstName) errors.firstName = "Please enter your first name.";
     if (!formValues.lastName) errors.lastName = "Please enter your last name.";
    if (!formValues.email) errors.email = "Please enter your email address.";
    else if (!regexEmail.test(formValues.email))
      errors.email = "Email is Invalid!";
    
    if (!formValues.password) errors.password = "Please enter password.";
    if (!formValues.confirmPassword)
      errors.confirmPassword = "Please enter confirm password.";
    else if (formValues.password !== formValues.confirmPassword)
      errors.confirmPassword = "Not Match!";
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
      createAPIEndpoint(EndPoints.Account.register)
        .post(values)
        .then((res) => {
          navigate("/login");
          enqueueSnackbar("Rigster Success.", { variant: "success" });
        })
        .catch((err) => { enqueueSnackbar("Login Failed.", { variant: "error" }); console.log(err)});
      resetForm();
    }
  }, [errors]);

  return (
    <div className="Register">
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
              Register
            </Typography>
            <form autoComplete="off" onSubmit={handleSubmitForm} noValidate>
              <Stack sx={{ gap: 2 }}>
                <Stack direction="row" sx={{gap: 3}}>
                  <FormControl variant="standard">
                    <Input
                      placeholder="First Name"
                      type="text"
                      name="firstName"
                      value={values.firstName}
                      onChange={handleInputChanged}
                      required
                    />
                    <p className="err-hint">{errors.firstName}</p>
                  </FormControl>
                  <FormControl variant="standard">
                    <Input
                      placeholder="Last Name"
                      type="text"
                      name="lastName"
                      value={values.lastName}
                      onChange={handleInputChanged}
                      required
                    />
                    <p className="err-hint">{errors.lastName}</p>
                  </FormControl>
                </Stack>

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

                <FormControl variant="standard">
                  <Input
                    placeholder="Confirm Password"
                    type={showConfirmPassword ? "text" : "password"}
                    name="confirmPassword"
                    value={values.confirmPassword}
                    onChange={handleInputChanged}
                    required
                    endAdornment={
                      <InputAdornment position="end">
                        <IconButton
                          aria-label="toggle password visibility"
                          onClick={handleClickShowConfirmPassword}
                          onMouseDown={handleMouseDownPassword}
                          onMouseUp={handleMouseUpPassword}
                        >
                          {showConfirmPassword ? (
                            <VisibilityOff />
                          ) : (
                            <Visibility />
                          )}
                        </IconButton>
                      </InputAdornment>
                    }
                  />
                  <p className="err-hint">{errors.confirmPassword}</p>
                </FormControl>
                <Button variant="contained" type="submit">
                  Submit
                </Button>
                <span className="nav-hint">
                  Already have an account?{" "}
                  <Typography component={Link} to="/" color="primary">
                    Login
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

export default Register;
