const express = require("express");
const router = express.Router();
const passport = require("passport");

// Game model
const Game = require("../../models/Game");

// Post
router.post("/", (req, res, next) => {
    // todo
});

// Get by game id
router.get("/:id", (req, res, next) => {
    // todo
});

// get all self
router.get(
  "/",
  passport.authenticate("jwt", { session: false }),
  (req, res, next) => {
      // todo
  }
);

// Put
router.put('/:id', passport.authenticate('jwt',{session:false}), (req,res,next)=>{
    // todo
});

// Delete
router.delete('/:id', passport.authenticate('jwt', {session:false}), (req,res,next)=>{
    // todo
});