using Graphs

graph = simple_graph(7,  is_directed=false)
add_edge!(graph, 1, 2)
add_edge!(graph, 1, 3)
add_edge!(graph, 2, 4)
add_edge!(graph, 2, 5)
add_edge!(graph, 3, 6)
add_edge!(graph, 2, 7)
n = num_vertices(graph)
channels = [Channel{Tuple{Int, Int}}(out_degree(i, graph)) for i=1:n]
parents = zeros(n)
children = [Set{Int}() for i=1:n]

#msg = 0 - false, 1 - true, 2 - look up

function rootProcess(id)
    parents[id] = id
    neighbourList = out_neighbors(id, graph)
    for neighbour in neighbourList
        println("id: $(id), to: $(neighbour)")
        put!(channels[neighbour], (2, id))
    end

    received = 0
    expected = 2length(out_neighbors(id, graph))

    while received < expected
        (msg, sender) = take!(channels[id])
        received = received + 1
        if msg == 2
            println("ROOT NO")
            put!(channels[sender], (0, id))
        elseif msg == 1
            println("ROOT YES")
            println(sender)
            push!(children[id], sender)
        end
    end
    println("Koniec $(id)")
end

function regularProcess(id)
    received = 0
    expected = 2length(out_neighbors(id, graph))
    println("MojeID : $(id) expected: $(expected)")
    while received < expected
        (msg, sender) = take!(channels[id])
        println("Sender $(sender)")
        received += 1
        if msg == 2
            if parents[id] == 0
                put!(channels[sender], (1, id))
                parents[id] = sender

                neighbourList = out_neighbors(id, graph)

                for neighbour in neighbourList
                    println("SEARCH FROM ID $(id) $(neighbour)")
                    put!(channels[neighbour], (2, id))
                end
            else
                println("NORM NO $(id)")
                put!(channels[sender], (0, id))
            end
        elseif msg == 1
            println("NORM YES $(id)")
            push!(childrens[id], sender)
        end
    end
    println("Koniec $(id)")
end

function st(graph)
    root = 1
    @sync for id=1 : n
        if id == root
            @async rootProcess(id)
        else
            @async regularProcess(id)
        end
    end
end


ch, p = st(graph)
println("CH: $(ch)")
println("P: $(p)")
