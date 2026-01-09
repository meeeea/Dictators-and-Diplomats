void Main() {
    PerlinMap mapsGen = new(1600, 900, 5, 5);

    Draw.BMPDraw(mapsGen);
}



Main();