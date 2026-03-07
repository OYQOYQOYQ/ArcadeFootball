extends CollisionShape2D


var height: float = 0


func _process(_delta):
    if shape is CapsuleShape2D:
        if height == shape.height:
            return
        height = shape.height;
        print("高度: ", height)