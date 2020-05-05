const mongoose = require("mongoose");

const GameSchema = new mongoose.Schema({
  players: { type: [String], required: true },
  scores: {
    type: Map,
    of: [Number],
  },
  winner: String,
  completed: Boolean,
  date: {
    type: Date,
    default: Date.now,
  },
  name: {
      type: String,
      required: true
  }
});

const Game = mongoose.model('Game', GameSchema);
module.exports = Game;
