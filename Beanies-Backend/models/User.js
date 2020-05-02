const mongoose = require("mongoose");

const UserSchema = new mongoose.Schema({
  name: {
    type: String,
    required: true,
  },
  email: {
    type: String,
    required: false,
  },
  password: {
    type: String,
    required: false,
  },
  guest:{
    type: Boolean
  },
  date: {
    type: Date,
    default: Date.now,
  }
});

const User = mongoose.model('User', UserSchema);
module.exports = User;
