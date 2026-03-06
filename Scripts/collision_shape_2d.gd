extends CollisionShape2D


func _physics_process(_delta):
    if shape is CapsuleShape2D:
        print("半径: ", shape.radius)
        print("高度: ", shape.height)