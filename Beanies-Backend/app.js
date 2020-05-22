require('dotenv').config()

const express = require("express");
const logger = require("./middleware/logger");
const expressLayouts = require('express-ejs-layouts');
const mongoose = require('mongoose');
const flash = require('connect-flash');
const session = require('express-session');
const passport = require('passport');

const app = express();

// Passport config
require('./config/passport')(passport);

// DB Config
const db = require('./config/keys').MongoURI;

// Connect to Mongo
mongoose.connect(db,{useNewUrlParser : true})
.then(()=>console.log('MongoDB connected...'))
.catch(err => console.log(err));

// EJS
app.use(expressLayouts);
app.set('view engine', 'ejs');

// Express session
app.use(
    session({
      secret: 'secret',
      resave: true,
      saveUninitialized: true
    })
  );

  // Passport middleware
app.use(passport.initialize());
app.use(passport.session());

// Connect flash
app.use(flash());

// init middleware
app.use(function(req, res, next) {
    res.locals.success_msg = req.flash('success_msg');
    res.locals.error_msg = req.flash('error_msg');
    res.locals.error = req.flash('error');
    next();
  });
app.use(logger);
app.use(express.json());
app.use(express.urlencoded({extended: false}));

// cms routes
app.use('/users', require('./routes/cms/users'));
app.use('/', require('./routes/cms/index'));

// api routes
app.use('/api/users', require('./routes/api/users'));
app.use('/api/games', require('./routes/api/games'));

module.exports = app;
