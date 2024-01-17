var encounter = null;
var canvas = null;

class Encounter {
    constructor(width, height, color) {
        this.width = width;
        this.height = height;
        this.color = color;

        this.engine = new BABYLON.Engine(canvas, true, { preserveDrawingBuffer: true, stencil: true });
        this.scene = new BABYLON.Scene(this.engine);
        this.scene.clearColor = this.color;

        //const axes = new BABYLON.Debug.AxesViewer(this.scene, 10);

        this.camera = new BABYLON.ArcRotateCamera("camera", 0, 50, 0, new BABYLON.Vector3(0, 50, 0), this.scene);
        this.camera.setTarget(BABYLON.Vector3.Zero());
        this.camera.attachControl(canvas, true);

        var light = new BABYLON.HemisphericLight('light', new BABYLON.Vector3(0, 20, 0), this.scene);
        light.specular = new BABYLON.Color3.Black();

        //grid
        this.groundMaterial = new BABYLON.GridMaterial("defaultGrid", this.scene);
        this.groundMaterial.majorUnitFrequency = 1;
        this.groundMaterial.gridRatio = 1;
        this.groundMaterial.lineColor = new BABYLON.Color3(255, 255, 255);
        this.groundMaterial.opacity = 0.95;

        this.ground1 = BABYLON.MeshBuilder.CreateGround(
            "ground",
            {
                width: this.width,
                height: this.height,
                subdivisions: 1,
                updatable: true
            },
            this.scene);

        this.ground1.material = this.groundMaterial;
        this.ground1.position.y += 0.05;

        //background
        this.backgroundMaterial = new BABYLON.StandardMaterial("background", this.scene);
        this.backgroundMaterial.diffuseTexture = new BABYLON.Texture("data/tex2.jpg", this.scene);

        this.ground2 = BABYLON.MeshBuilder.CreateGround(
            "ground2",
            {
                width: this.width,
                height: this.height,
                subdivisions: 1,
                updatable: true
            },
            this.scene);
        this.ground2.material = this.backgroundMaterial;
    }

    changeGroundSize(width = 100, height = 100) {
        var s = new BABYLON.Vector3(width / this.width, 1, height / this.height);
        this.ground1.scaling = s;
        this.ground2.scaling = s;
    }

    changeTileSize(tileSize) {
        this.groundMaterial.gridRatio = tileSize;
    }
}

var runEncounter = function () {
    canvas = document.getElementById('encounterCanvas');
    encounter = new Encounter(250, 250, BABYLON.Color3.Black());

    encounter.engine.runRenderLoop(function () {
        encounter.scene.render();
    });

    window.addEventListener('resize', function () {
        encounter.engine.resize();
    });
}

var changeGroundTexture = function (image) {
    if (image !== undefined) {
        const reader = new FileReader();
        reader.readAsDataURL(image);

        reader.onload = (_event) => {
            encounter.backgroundMaterial.diffuseTexture = new BABYLON.Texture(reader.result, encounter.scene);
        };
    }
}

