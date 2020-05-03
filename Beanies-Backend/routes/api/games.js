const express = require("express");
const router = express.Router();
const passport = require("passport");

// Game model
const Game = require("../../models/Game");
const User = require("../../models/User");

// Post
router.post("/", async (req, res, next) => {
  const { name, players } = req.body;
  if (!name || !players) {
    res
      .status(401)
      .json({ success: false, msg: "must provide name and players" });
  }

  const scores = new Map();

  var invalidUsers = [];
  for (let index = 0; index < players.length; index++) {
    const element = players[index];
    await User.findOne({ _id: element })
      .then((user) => {
        if (!user) {
          // check for invalid users
          invalidUsers.push(element);
        } else {
          // initialize scoreboard
          scores.set(element, []);
        }
      })
      .catch((err) => next(err));
  }
  if (invalidUsers.length > 0) {
    res
      .status(401)
      .json({ success: false, msg: `no user found with id: ${invalidUsers}` });
  } else {
    const newGame = new Game({
      name: name,
      scores: scores,
    });
    newGame
      .save()
      .then((game) => {
        res.status(200).json({ success: true, game: game });
      })
      .catch((err) => next(err));
  }
});

// Get by game id
router.get("/:id", (req, res, next) => {
  Game.findOne({ _id: req.params.id })
    .then((game) => {
      if (!game) {
          res.status(404).json({success:false,msg:`unable to find game with id: ${req.params.id}`});
      } else {
        res.status(200).json({
          success: true,
          game: game,
        });
      }
    })
    .catch((err) => {
        res.status(404).json({success:false,msg:`unable to find game with id: ${req.params.id}`});
        next(err);
    });
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
router.put(
  "/:id",
  passport.authenticate("jwt", { session: false }),
  (req, res, next) => {
    // todo
  }
);

// Delete
router.delete(
  "/:id",
  passport.authenticate("jwt", { session: false }),
  (req, res, next) => {
    // todo
  }
);

module.exports = router;
