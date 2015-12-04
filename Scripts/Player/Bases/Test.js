#pragma strict

function Start () {

}

function Update () {
    if (Input.GetKey(KeyCode.A)) {
        test();
    }
}
function test(){
    print(Time.time);
    yield WaitForSeconds(5);
    print(Time.time);
}