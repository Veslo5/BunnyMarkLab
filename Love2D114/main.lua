local bunnyTexture = nil
local bunnyTextureWidth, bunnyTextureHeight
local screenWidth, screenHeight

local bunniesCount = 0
local bunnies = {}

function love.load()
    bunnyTexture = love.graphics.newImage("wabbit_alpha.png")
    bunnyTexture:setFilter("nearest", "nearest")
    bunnyTextureWidth = bunnyTexture:getWidth()
    bunnyTextureHeight = bunnyTexture:getHeight()

    screenWidth = love.graphics.getWidth()
    screenHeight = love.graphics.getHeight()

    for i = 1, 200000, 1 do
        table.insert(bunnies, {
            x = love.mouse.getX(),
            y = love.mouse.getY(),
            speedx = love.math.random(-250, 250) / 60.0,
            speedy = love.math.random(-250, 250) / 60.0,
            r = 1 / love.math.random(50, 255),
            g = 1 / love.math.random(80, 255),
            b = 1 / love.math.random(100, 255)
        })
        bunniesCount = bunniesCount + 1
    end
end

function love.update()
    if love.mouse.isDown(1) then
        for i = 1, 101, 1 do
            table.insert(bunnies, {
                x = love.mouse.getX(),
                y = love.mouse.getY(),
                speedx = love.math.random(-250, 250) / 60.0,
                speedy = love.math.random(-250, 250) / 60.0,
                r = 1 / love.math.random(50, 255),
                g = 1 / love.math.random(80, 255),
                b = 1 / love.math.random(100, 255)
            })
            bunniesCount = bunniesCount + 1
        end
    end

    -- for _, bunny in ipairs(bunnies) do
    for i = 1, bunniesCount, 1 do
        local bunny = bunnies[i]

        bunny.x = bunny.x + bunny.speedx
        bunny.y = bunny.y + bunny.speedy

        if (bunny.x + bunnyTextureWidth / 2) > screenWidth or (bunny.x + bunnyTextureWidth / 2) < 0 then
            bunny.speedx = bunny.speedx * -1
        end

        if (bunny.y + bunnyTextureHeight / 2) > screenHeight or (bunny.x + bunnyTextureHeight / 2) < 0 then
            bunny.speedy = bunny.speedy * -1
        end
    end
end

function love.draw()
    love.graphics.setBackgroundColor(1, 1, 1, 1)

    love.graphics.setColor(1, 1, 1)

    -- for _, bunny in ipairs(bunnies) do
    for i = 1, bunniesCount, 1 do
        local bunny = bunnies[i]
        --love.graphics.setColor(bunny.r, bunny.g, bunny.b, 1)
        love.graphics.draw(bunnyTexture, bunny.x, bunny.y)
    end

    love.graphics.setColor(0, 0, 0, 1)

    love.graphics.print(love.timer.getFPS(), 0, 0)
    love.graphics.print(bunniesCount, 0, 10)
end
