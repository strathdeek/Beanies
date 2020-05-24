const express = require("express");
const router = express.Router();
const passport = require("passport");
const bcrypt = require("bcryptjs");
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
  if (!name || !email || !password) {
    errors.push({ msg: "Must provide email, password, and name" });
    res.status(401).json({ success: false, errors: errors });
  }

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
        guest: false,
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

// register guest
router.post("/guest", (req, res, next) => {
  const { name } = req.body;
  let errors = [];
  if (!name) {
    errors.push({ msg: "Must provide name" });
    res.status(401).json({ success: false, errors: errors });
  }
  const newUser = new User({
    name: name,
    guest: true,
  });
  newUser
    .save()
    .then((user) => {
      res.status(200).json({
        success: true,
        user: user,
      });
    })
    .catch((err) => next(err));
});

// register user from guest
router.post("/register/:id", (req, res, next) => {
  const { name, email, password } = req.body;
  let errors = [];
  if (!email || !password) {
    errors.push({ msg: "Must provide email, and password" });
    res.status(401).json({ success: false, errors: errors });
  }

  User.findOne({ email: email }).then((user) => {
    if (user) {
      errors.push({
        msg: "User email is already registered",
      });
      res.status(401).json({ success: false, errors: errors });
    } else {
      User.findOne({ _id: req.params.id })
        .then((userToUpdate) => {
          if (!userToUpdate) {
            res
              .status(401)
              .json({ success: false, msg: "Could not find user" });
          }
          if (name && name !== userToUpdate.name) {
            userToUpdate.name = name;
          }
          userToUpdate.email = email;
          userToUpdate.guest = false;
          // Hash password
          bcrypt.genSalt(10, (err, salt) =>
            bcrypt.hash(password, salt, (err, hash) => {
              if (err) throw err;
              userToUpdate.password = hash;
            })
          );
          userToUpdate
            .save()
            .then((user) => {
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
        .catch((err) => {
          res.status(401).json({ success: false, msg: "Could not find user" });
          next(err);
        });
    }
  });
});

// get self
router.get(
  "/",
  passport.authenticate("jwt", { session: false }),
  (req, res, next) => {
    res
      .status(200)
      .json({ success: true, msg: "You are authorized", user: req.user });
  }
);

//get by id
router.get("/:id", (req, res, next) => {
  User.findOne({ _id: req.params.id })
    .then((user) => {
      if (!user) {
        res.status(401).json({ success: false, msg: "Could not find user" });
      }
      res.status(200).json({
        success: true,
        user: {
          _id: user._id,
          name: user.name,
          date: user.date,
          guest: user.guest,
        },
      });
    })
    .catch((err) => {
      res.status(401).json({ success: false, msg: "Could not find user" });
      next(err);
    });
});

//put self
router.put(
  "/",
  passport.authenticate("jwt", { session: false }),
  (req, res, next) => {
    const { name, email, password } = req.body;
    User.findOne({ _id: req.user._id })
      .then((userToUpdate) => {
        if (name && name !== userToUpdate.name) {
          userToUpdate.name = name;
        }
        if (email && email !== userToUpdate.email) {
          userToUpdate.email = email;
        }
        bcrypt.compare(password, userToUpdate.password).then((match) => {
          if (match !== true) {
            // Hash password
            bcrypt.genSalt(10, (err, salt) =>
              bcrypt.hash(password, salt, (err, hash) => {
                if (err) throw err;
                userToUpdate.password = hash;
              })
            );
          }
        });
        userToUpdate
          .save()
          .then((user) => {
            res.status(200).json({
              success: true,
              user: user,
            });
          })
          .catch((err) => next(err));
      })
      .catch((err) => next(err));
  }
);

// put guest
router.put(
  "/:id",
  passport.authenticate("jwt", { session: false }),
  (req, res, next) => {
    const { name } = req.body;
    User.findOne({ _id: req.params.id })
      .then((user) => {
        if (!user) {
          res.status(401).json({ success: false, msg: "Could not find user" });
        }
        if (user.guest === false) {
          res.status(401).json({
            success: false,
            msg: "Update only possible on guest accounts",
          });
        }
        if (name && name !== user.name) {
        }
        user.name = name;
        user
          .save()
          .then((user) => {
            res.status(200).json({
              success: true,
              user: user,
            });
          })
          .catch((err) => next(err));
      })
      .catch((err) => {
        res.status(401).json({ success: false, msg: "Could not find user" });
        next(err);
      });
  }
);

// delete
router.delete('/:id',passport.authenticate('jwt',{session:false}),(req,res,next)=>{
  User.deleteOne({_id: req.params.id})
  .then((user) =>{
    if(user.deletedCount >= 1){
      res.status(200).json({success:true});
    } else {
      res.status(404).json({success: false, msg: `user with id ${req.params.id} not found`});
    }
  })
  .catch(err => next(err));
});

// export
module.exports = router;
