const express = require("express");
const path = require("path");
const logger = require("./middleware/logger");

const app = express();

const PORT = process.env.PORT || 5000;

app.use(express.static(path.join(__dirname, "public")));

// init middleware
app.use(logger);
app.use(express.json());
app.use(express.urlencoded({extended: false}));

// user api routes
app.use('/api/user', require('./routes/api/user'));


app.listen(PORT, () => console.log(`Server Started on Port ${PORT}`));
