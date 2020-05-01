const express = require("express");
const router = express.Router();
const bcrypt = require("bcrypt");
const passport = require("passport");
// User model
const User = require("../models/User");

// login
router.get("/login", (req, res) => {
  res.render("login");
});

router.post("/login", (req, res,next) => {
  passport.authenticate('local', {
    successRedirect: '/dashboard',
    failureRedirect: '/users/login',
    failureFlash: true
  })(req,res,next);
});

// register get
router.get("/register", (req, res) => {
  res.render("register");
});

// register post
router.post("/register", (req, res) => {
  const { name, email, password, password2 } = req.body;
  let errors = [];

  // check all fields
  if (!name || !email || !password || !password2) {
    errors.push({ msg: "Please ensure all fields are filled" });
  }

  // check password match
  if (password !== password2) {
    errors.push({ msg: "Passwords do not match" });
  }

  // check password length
  if (password.length < 6) {
    errors.push({ msg: "Password should be at least 6 characters" });
  }

  if (errors.length > 0) {
    res.render("register", {
      errors,
      name,
      email,
      password,
      password2,
    });
  } else {
    User.findOne({ email: email }).then((user) => {
      if (user) {
        errors.push({
          msg: "User email is already registered",
        });
        res.render("register", {
          errors,
          name,
          email,
          password,
          password2,
        });
      } else {
        const newUser = new User({
          name,
          email,
          password,
        });

        // Hash password
        bcrypt.genSalt(10, (err, salt) =>
          bcrypt.hash(newUser.password, salt, (err, hash) => {
            if (err) throw err;
            newUser.password = hash;
            newUser
              .save()
              .then((user) => {
                req.flash(
                  "success_msg",
                  "You are now registered and can log in"
                );
                res.redirect("/users/login");
              })
              .catch((err) => console.log(err));
          })
        );
      }
    });
  }
});

// Logout 

router.get('/logout',(req,res)=> {
  req.logout();
  req.flash('success_message', 'You have successfully logged out');
  res.redirect('/users/login');
});

//export
module.exports = router;
