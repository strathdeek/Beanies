const express = require("express");
const router = express.Router();
const users = require("../../Users");
const uuid = require("uuid");

//get all users
router.get("/", (req, res) => res.json(users));

// get a user
router.get("/:id", (req, res) => {
  const found = users.some((user) => user.id === parseInt(req.params.id));
  if (found) {
    res.json(users.filter((user) => user.id === parseInt(req.params.id)));
  } else {
    res
      .status(400)
      .json({ msg: `no user with the id of ${req.params.id} found` });
  }
});

// create a user
router.post("/", (req, res) => {
  const newUser = {
    id: uuid.v4(),
    name: req.body.name,
    played: 0,
    won: 0,
  };

  if (!newUser.name) {
    res.status(500).json({ msg: "please include a name" });
  }
  users.push(newUser);

  res.json(users);
});

// update a user
router.put("/:id", (req, res) => {
  const found = users.some((user) => user.id === parseInt(req.params.id));
  if (found) {
      const updateUser = req.body;
      users.forEach(user => {
          if(user.id === parseInt(req.params.id)){
              user.name = updateUser.name ? updateUser.name : user.name;
              user.played = updateUser.played ? updateUser.played : user.played;
              user.won = updateUser.won ? updateUser.won : user.won;

              res.json({msg: "User updated ", user });
          }
      });
    res.json(users.filter((user) => user.id === parseInt(req.params.id)));
  } else {
    res
      .status(400)
      .json({ msg: `no user with the id of ${req.params.id} found` });
  }
});

// delete
router.delete('/:id', (req, res) => {
    const found = users.some((user) => user.id === parseInt(req.params.id));
    if (found) {
        res.json({msg: "User Deleted", users: users.filter((user) => user.id !== parseInt(req.params.id))});
    }
})

//export
module.exports = router;
