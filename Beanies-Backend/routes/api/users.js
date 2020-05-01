const express = require("express");
const router = express.Router();
const passport = require("passport");
const bcrypt = require("bcrypt");
const utils = require("../../lib/utils");

// User model
const User = require("../../models/User");

// login
router.post("/login", (req, res, next) => {
  const { email, password } = req.body;
  let errors = [];
  User.findOne({ email: email })
    .then((user) => {
      if (!user) {
        errors.push({
          msg: "User does not exist",
        });
        res.status(401).json({ success: false, errors: errors });
      }
      bcrypt
        .compare(password, user.password)
        .then((match) => {
          if (match === true) {
            const id = user._id;
            const jwt = utils.issueJWT(user);
            res.status(200).json({
              success: true,
              user: user,
              token: jwt.token,
              expiresIn: jwt.expires,
            });
            // success
          } else {
            errors.push({
              msg: "Incorrect password",
            });
            res.status(401).json({ success: false, errors: errors });
          }
        })
        .catch((err) => next(err));
    })
    .catch((err) => console.log(err));
});

// register
router.post("/register", (req, res, next) => {
  const { name, email, password } = req.body;
  let errors = [];

  User.findOne({ email: email }).then((user) => {
    if (user) {
      errors.push({
        msg: "User email is already registered",
      });
      res.status(401).json({ success: false, errors: errors });
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
              const id = user._id;
              const jwt = utils.issueJWT(user);
              res.status(200).json({
                success: true,
                user: user,
                token: jwt.token,
                expiresIn: jwt.expires,
              });
            })
            .catch((err) => next(err));
        })
      );
    }
  });
});

// protected
router.get("/protected", passport.authenticate('jwt', {session: false}), (req, res, next) => {
    res.status(200).json({success: true, msg: "You are authorized"});
});

// export
module.exports = router;
