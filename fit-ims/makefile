CXX=g++
CXXFLAGS=-g -Wall -Wextra -pedantic -std=c++17 -O3
SOURCES:=$(wildcard ./src/*.cpp)
HEADERS:=$(wildcard ./src/*.hpp)
OBJECTS:=$(SOURCES:./src/%.cpp=./build/%.o)

EXE=ims
DOC=zprava.pdf
ZIP=01-xvever13-xwagne10.zip
MAN=

.PHONY: clean zip tar run

############################################
# PROGRAM

$(EXE): $(OBJECTS)
	$(CXX) $^ -o $@


# TODO every file has its needs (ungeneralize)
./build/%.o: ./src/%.cpp ./src/%.hpp $(HEADERS)
	mkdir -p build
	$(CXX) $(CXXFLAGS) -c $< -o $@


############################################
# DOCUMENTATION

$(DOC): ./doc/zprava.tex ./doc/zprava.references.bib
	pdflatex -output-directory=doc $<
	pdflatex -output-directory=doc $<
	biber ./doc/zprava
	pdflatex -output-directory=doc $<
	pdflatex -output-directory=doc $<
	mv ./doc/zprava.pdf ./zprava.pdf


############################################
# MISC

pack: 
	rm -f $(ZIP)
	zip -r $(ZIP) * 	

clean:
	rm -f $(OBJECTS) $(ZIP) $(EXE) $(DOC)
	-cd ./doc && rm -f *.log *.out *.aux *.bbl *.blg *-blx.bib *.xml *.toc *.bcf

run: $(EXE)
	./ims -f simdata/sim_random.txt -m 10000 -s 1000
