extends CharacterBody2D

@export var SPEED: float = 400

var xdir: float = 1

func _ready():
	# print("I'm Alive!")
	pass
	

func _physics_process(_delta: float) -> void:
	velocity.x = xdir * SPEED
	
	move_and_slide()

func _on_area_2d_body_entered(body: Node2D) -> void:
	# print("I'm Dead!")
	queue_free()
