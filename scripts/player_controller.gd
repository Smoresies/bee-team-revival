extends CharacterBody2D

@export var MAX_SPEED: int = 200
@export var JUMP_VELOCITY: int = -300
@export var ACCELERATION: int = 1500
@export var DASH_POWER: int = 2000
@export_range(0, 1, 0.05) var DASH_TIME: float = 0.1
@export_range(0, 10, 0.1, "or_greater") var DECELERATION_TIME: float = 0.1

var deceleration: float = MAX_SPEED / DECELERATION_TIME

var horizontal_facing: int = 1

var double_jump: bool = true
var grounded: bool = true
var can_dash: bool = true
var dashing: bool = false

@export var bullet: PackedScene

func _physics_process(delta: float) -> void:
	apply_gravity(delta)
	
	apply_player_jump()
	
	apply_horizontal_movement(delta)
	
	apply_player_dash()
	
	apply_player_shoot()
	
	# Test code for One-Way Platform
	if Input.is_action_pressed("ui_down"):
		set_collision_mask_value(5, false)
	else:
		set_collision_mask_value(5, true)

	move_and_slide()


func apply_gravity(delta: float):
	# Add the apply_gravity.
	if not is_on_floor():
		if not dashing:
			grounded = false
			velocity += get_gravity() * delta
	# this elif checks if we are on floor and weren't last tick
	elif grounded == false:
		_update_grounded_flags()

# Checks for jump input and jumps if either grounded or have a double jump
# Using local boolean rather than is_on_floor function call. 
# Likely unneeded Micro-Optimization, but is used for other checks as well
func apply_player_jump():
	if Input.is_action_just_pressed("jump"):
		if grounded:
			velocity.y = JUMP_VELOCITY
		elif double_jump:
			velocity.y = JUMP_VELOCITY
			double_jump = false

func apply_horizontal_movement(delta: float):
	
	
	if not dashing:
		# Get the input direction and handle the movement/deceleration.
		var direction := Input.get_axis("left", "right")

		if direction > 0:
			horizontal_facing = 1
		elif direction < 0:
			horizontal_facing = -1
		# In case of overspeed from dash
		if abs(velocity.x) > MAX_SPEED:
			velocity.x = (horizontal_facing * MAX_SPEED) / 2.0
		if direction: # Direction button active
			# Accelerate towards the target speed
			velocity.x = move_toward(velocity.x, direction * MAX_SPEED, ACCELERATION * delta)
		else: # Direction button inactive
			# Decelerate towards zero speed
			velocity.x = lerp(velocity.x, 0.0, DECELERATION_TIME)

func apply_player_dash():
	if Input.is_action_just_pressed("dash") and can_dash:
		can_dash = false
		dashing = true
		velocity.x = horizontal_facing * DASH_POWER
		velocity.y = 0
		await get_tree().create_timer(0.1).timeout
		if grounded:
			can_dash = true
		dashing = false 

func apply_player_shoot():
	if Input.is_action_just_pressed("shoot"):
		# print("Shooting!")
		var instance = bullet.instantiate()
		
		instance.position = global_position
		instance.xdir = horizontal_facing
		
		get_tree().root.add_child(instance)

# Called when we have just become grounded. Updates all flags for being grounded.
func _update_grounded_flags():
	grounded = true
	double_jump = true
	if dashing == false:
		can_dash = true
