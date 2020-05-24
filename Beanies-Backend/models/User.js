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

UserSchema.methods.toJSON = function() {
  var obj = this.toObject();
  delete obj.password;
  return obj;
 }

 
const User = mongoose.model('User', UserSchema);
module.exports = User;
